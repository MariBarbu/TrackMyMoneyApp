using AutoMapper;
using DataLayer;
using DataLayer.Entities;
using DataLayer.Repositories;
using Services.Dtos.Spending;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface ISpendingService
    {
        Task<bool> AddSpendingAsync(AddSpendingDto spending, MoneyUser moneyUser);
        GetSpendingsDto GetSpendingsByCategoryAndUser(Guid categoryId, MoneyUser moneyUser);
        GetSpendingsDto GetSpendingsByMonthAndUser(Guid categoryId, MoneyUser moneyUser);
    }
    public class SpendingService : ISpendingService { 
    
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public SpendingService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> AddSpendingAsync(AddSpendingDto spending, MoneyUser moneyUser)
        {
            if (moneyUser == null)
                throw new BadRequestException(ErrorService.NoUserFound);
            var currentMonth = _unitOfWork.Months.GetCurrentMonth(moneyUser.Id);
            var moneySpent = currentMonth.Spendings.Sum(s => s.Cost);

            if (spending.Id != null)
            {
                var oldSpending = await _unitOfWork.Spendings.DbGetByIdAsync((Guid)spending.Id);
                if (oldSpending == null)
                    throw new BadRequestException(ErrorService.SpendingNotFound);
                if (oldSpending.Month.MonthOfYear != DateTime.UtcNow.Month || oldSpending.Month.Year != DateTime.UtcNow.Year)
                    throw new BadRequestException(ErrorService.SpendingExpired);
                
                if (moneySpent - oldSpending.Cost + spending.Cost > currentMonth.Budget)
                    throw new BadRequestException(ErrorService.NotEnoughMoney);
                oldSpending.CategoryId = spending.CategoryId;
                oldSpending.Cost = spending.Cost;
                oldSpending.Details = spending.Details;
                _unitOfWork.Spendings.Update(oldSpending);                 
            }
            else
            {
                var newSpending = _mapper.Map<Spending>(spending);
                if (moneySpent + spending.Cost > currentMonth.Budget)
                    throw new BadRequestException(ErrorService.NotEnoughMoney);
                newSpending.MonthId = currentMonth.Id;
                _unitOfWork.Spendings.Insert(newSpending);
            }

            return await _unitOfWork.SaveChangesAsync();
        }

        public GetSpendingsDto GetSpendingsByCategoryAndUser(Guid categoryId, MoneyUser moneyUser)
        {
            if (moneyUser == null)
                throw new BadRequestException(ErrorService.NoUserFound);
            var result = new GetSpendingsDto();
            var category = _unitOfWork.Categories.GetWithSpendings(categoryId);
            var spendingsDto = _mapper.Map<List<GetSpendingDto>>(category.Spendings);
            var orderedSpendings = spendingsDto.OrderByDescending(s => s.CreatedAt);
            result.Spendings = spendingsDto;
            return result;
        }

            public GetSpendingsDto GetSpendingsByMonthAndUser(Guid categoryId, MoneyUser moneyUser)
            {
                if (moneyUser == null)
                    throw new BadRequestException(ErrorService.NoUserFound);
                var result = new GetSpendingsDto();
                var month = _unitOfWork.Months.GetCurrentMonth(moneyUser.Id);
                var spendingsDto = _mapper.Map<List<GetSpendingDto>>(month.Spendings);
                var orderedSpendings = spendingsDto.OrderByDescending(s => s.CreatedAt);
                result.Spendings = spendingsDto;
                return result;
            }
        }
}
