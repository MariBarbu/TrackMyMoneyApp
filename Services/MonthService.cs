using DataLayer;
using DataLayer.Entities;
using DataLayer.Repositories;
using Services.Dtos.Month;
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
    }
    public class MonthService : IMonthService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MonthService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AddEconomy (MoneyUser moneyUser, AddEconomyDto economy)
        {
            if (moneyUser == null)
                throw new BadRequestException(ErrorService.NoUserFound);
            moneyUser.Economies += economy.Economy;
            _unitOfWork.MoneyUsers.Update(moneyUser);
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdateBudget(MoneyUser moneyUser, UpdateBudgetDto budget)
        {
            if (moneyUser == null)
                throw new BadRequestException(ErrorService.NoUserFound);
            var currentMonth = _unitOfWork.Months.GetCurrentMonth(moneyUser.Id);
            var moneySpent = currentMonth.Spendings.Sum(s => s.Cost);
            if (moneySpent > budget.Budget)
                throw new BadRequestException(ErrorService.NotEnoughMoney);
            currentMonth.Budget += budget.Budget;
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
    }
}
