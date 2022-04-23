using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinApp.Models;
using XamarinApp.Services;


namespace XamarinApp.ViewModels.Month
{
    [QueryProperty(nameof(Year), nameof(Year))]
    public class YearHistoryViewModel : BaseViewModel
    {
        private readonly IMonthService _monthService;
        public decimal budget;
        public decimal economies;
        public decimal totalSpent;
        public IList<Spending> spendings;
        public int year;
        public YearHistoryViewModel(IMonthService monthService)
        {
            _monthService = monthService;
            Title= "History By Year";
        }

      
        public async Task GetHistory()
        {
                var yearSelected = (year != 0) ? year : DateTime.UtcNow.Year; 

                var history = await _monthService.GetHistoryByYear(yearSelected);
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
    }
}
