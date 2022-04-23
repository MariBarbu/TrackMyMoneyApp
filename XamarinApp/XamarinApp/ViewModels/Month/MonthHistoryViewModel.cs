using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinApp.Models;
using XamarinApp.Services;

namespace XamarinApp.ViewModels.Month
{
    [QueryProperty(nameof(Year), nameof(Year))]
    [QueryProperty(nameof(Month), nameof(Month))]
    public class MonthHistoryViewModel : BaseViewModel
    {
        private readonly IMonthService _monthService;
        public decimal budget;
        public decimal economies;
        public decimal totalSpent;
        public IList<Spending> spendings;
        public int year;
        public int month;
    public MonthHistoryViewModel(IMonthService monthService)
        {
            _monthService = monthService;
            Title= "History By Month";
        }

      
        public async Task GetHistory()
        {
                var yearSelected = (year != 0) ? year : DateTime.UtcNow.Year; 
                var monthSelected = (month !=0) ? month : DateTime.UtcNow.Month;
                var history = await _monthService.GetHistoryByMonth(yearSelected, monthSelected);
                Budget = history.Budget;
                Economies = history.Economies;
                TotalSpent = history.TotalSpent;
                Spendings = history.Spendings;
            }

        
        public decimal Budget
        {
            get => budget;
            set => SetProperty(ref budget, value);
        }
        public decimal Economies
        {
            get => economies;
            set => SetProperty(ref economies, value);
        }
        public decimal TotalSpent
        {
            get => totalSpent;
            set => SetProperty(ref totalSpent, value);
        }
        public IList<Spending> Spendings
        {
            get => spendings;
            set => SetProperty(ref spendings, value);
        }

        public int Year
        {
            get => year;
            set => SetProperty(ref year, value);
        }
        public int Month
        {
            get => month;
            set => SetProperty(ref month, value);
        }

    }
}
