using AutoMapper;
using DataLayer;
using DataLayer.Entities;
using DataLayer.Repositories;
using Services.Dtos.Spending;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface ISpendingService
    {
        Task<bool> AddSpendingAsync(AddSpendingDto spending, MoneyUser moneyUser);
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
            if(spending.Id != null)
            {
                var oldSpending = await _unitOfWork.Spendings.DbGetByIdAsync((Guid)spending.Id);
                if (oldSpending == null)
                    throw new BadRequestException(ErrorService.SpendingNotFound);
                if (oldSpending.Month.MonthOfYear != DateTime.UtcNow.Month || oldSpending.Month.Year != DateTime.UtcNow.Year)
                    throw new BadRequestException(ErrorService.SpendingExpired);
                oldSpending.CategoryId = spending.CategoryId;
                oldSpending.Cost = spending.Cost;
                oldSpending.Details = spending.Details;
                _unitOfWork.Spendings.Update(oldSpending);                 
            }
            else
            {
                var newSpending = _mapper.Map<Spending>(spending);
                var currentMonth = _unitOfWork.Months.GetCurrentMonth();
                newSpending.MonthId = currentMonth.Id;
                _unitOfWork.Spendings.Insert(newSpending);
            }

            return await _unitOfWork.SaveChangesAsync();
        }
    }
}
