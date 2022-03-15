using DataLayer.Entities;
using DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobs
{
    public interface IMonthJob : IJob
    {
    }
    public class MonthJob : IMonthJob
    {
        private readonly IUnitOfWork _unitOfWork;

        public MonthJob(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Execute()
        {
            var year = DateTime.UtcNow.Year;
            var month = DateTime.UtcNow.Month;
            var day = DateTime.UtcNow.Day;

            var moneyUsers = _unitOfWork.MoneyUsers.GetAll();

            foreach(var moneyUser in moneyUsers)
            {
                CloseMonthAsync(moneyUser, year, month, day);
                OpenMonthAsync(moneyUser, year, month, day); 
            }
        }

        private void OpenMonthAsync(MoneyUser moneyUser, int year, int month, int day)
        {
            var currentMonth = _unitOfWork.Months.GetCurrentMonth(moneyUser.Id);
            if (currentMonth == null || (currentMonth.MonthOfYear != month || currentMonth.Year !=year) && day == 1)
            {
                var newMonth = new Month
                {
                    Year = year,
                    MonthOfYear = month,
                    MoneyUserId = moneyUser.Id
                };
                _unitOfWork.Months.Insert(newMonth);
                _unitOfWork.SaveChangesAsync().Wait();
            }
        }

        private void CloseMonthAsync(MoneyUser moneyUser, int year, int month, int day)
        {
            var currentMonth = _unitOfWork.Months.GetCurrentMonth(moneyUser.Id);
            if(currentMonth != null)
            {
                if ((currentMonth.MonthOfYear != month || currentMonth.Year !=year) && day == 1)
                {
                    var moneySpent = currentMonth.Spendings.Sum(s => s.Cost);
                    currentMonth.Economies = currentMonth.Budget - moneySpent;
                    moneyUser.Economies += currentMonth.Economies;
                    _unitOfWork.Months.Update(currentMonth);
                    _unitOfWork.MoneyUsers.Update(moneyUser);
                    _unitOfWork.SaveChangesAsync().Wait();
                }
            }
        }
    }

}
