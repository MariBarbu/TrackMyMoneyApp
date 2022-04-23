using AutoMapper;
using DataLayer;
using DataLayer.Entities;
using DataLayer.Repositories;
using Services.Dtos.Month;
using Services.Dtos.Spending;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IMonthService
    {
        Task<bool> AddEconomy(MoneyUser moneyUser, AddEconomyDto economy);
        Task<bool> UpdateBudget(MoneyUser moneyUser, UpdateBudgetDto budget);
        GetDefaultScreenDto GetDefaultScreen(MoneyUser moneyUser);
        HistoryDto GetHistoryByMonth(int year, int month, MoneyUser moneyUser);
        HistoryDto GetHistoryByYear(int year, MoneyUser moneyUser);
        UpdateBudgetDto GetBudget(MoneyUser moneyUser);
        List<int> GetYears(MoneyUser moneyUser);
    }
    public class MonthService : IMonthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MonthService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> AddEconomy (MoneyUser moneyUser, AddEconomyDto economy)
        {
            if (moneyUser == null)
                throw new BadRequestException(ErrorService.NoUserFound);
            moneyUser.Economies += economy.Economy;
            _unitOfWork.MoneyUsers.Update(moneyUser);
            return await _unitOfWork.SaveChangesAsync();
        }

        public UpdateBudgetDto GetBudget(MoneyUser moneyUser)
        {
            if (moneyUser == null)
                throw new BadRequestException(ErrorService.NoUserFound);
            var month = _unitOfWork.Months.GetCurrentMonth(moneyUser.Id);
            return new UpdateBudgetDto
            {
                Budget = month.Budget
            };
        }

        public async Task<bool> UpdateBudget(MoneyUser moneyUser, UpdateBudgetDto budget)
        {
            if (moneyUser == null)
                throw new BadRequestException(ErrorService.NoUserFound);
            var currentMonth = _unitOfWork.Months.GetCurrentMonth(moneyUser.Id);
            var moneySpent = currentMonth.Spendings.Sum(s => s.Cost);
            if (moneySpent > budget.Budget)
                throw new BadRequestException(ErrorService.NotEnoughMoney);
            currentMonth.Budget = budget.Budget;
            _unitOfWork.Months.Update(currentMonth);
            return await _unitOfWork.SaveChangesAsync();
        }

        public GetDefaultScreenDto GetDefaultScreen(MoneyUser moneyUser)
        {
            if (moneyUser == null)
                throw new BadRequestException(ErrorService.NoUserFound);
            var currentMonth = _unitOfWork.Months.GetCurrentMonth(moneyUser.Id);
            var moneySpent = currentMonth.Spendings.Sum(s => s.Cost);
            var result = new GetDefaultScreenDto
            {
                MonthId = currentMonth.Id,
                Budget = currentMonth.Budget,
                Spendings = moneySpent,
                Economies = currentMonth.Economies + currentMonth.Budget - moneySpent
            };
            return result;
        }

        public HistoryDto GetHistoryByMonth(int year, int month, MoneyUser moneyUser)
        {
            if (moneyUser == null)
                throw new BadRequestException(ErrorService.NoUserFound);
            var historyMonth = _unitOfWork.Months.GetMonthByYearAndMonth(year, month, moneyUser.Id);
            if (historyMonth == null)
                throw new BadRequestException(ErrorService.InvalidYearOrMonth);
            var result = new HistoryDto
            {
                Budget = historyMonth.Budget,
                Economies = historyMonth.Economies,
                TotalSpent = historyMonth.Spendings.Sum(s => s.Cost),
                Spendings = _mapper.Map<List<GetSpendingDto>>(historyMonth.Spendings)
            };
            return result;
        }

        public HistoryDto GetHistoryByYear(int year, MoneyUser moneyUser)
        {
            if (moneyUser == null)
                throw new BadRequestException(ErrorService.NoUserFound);
            var history = _unitOfWork.Months.GetMonthByYear(year, moneyUser.Id);
            if (history == null)
                throw new BadRequestException(ErrorService.InvalidYearOrMonth);
            var result = new HistoryDto
            {
                Budget = history.Sum(m => m.Budget),
                Economies = history.Sum(m => m.Economies),
                TotalSpent = history.Sum(m => m.Spendings.Sum(s => s.Cost)),
                Spendings = _mapper.Map<List<GetSpendingDto>>(history.SelectMany(m =>m.Spendings))
            };
            return result;
        }

        public List<int> GetYears(MoneyUser moneyUser)
        {
            if (moneyUser == null)
                throw new BadRequestException(ErrorService.NoUserFound);
            var history = _unitOfWork.Months.GetAllByUser(moneyUser.Id);
            if (history == null)
                throw new BadRequestException(ErrorService.NoHistory);
            var result =  history.Select(h => h.Year).Distinct().ToList();
            return result;

        }
    }
}
