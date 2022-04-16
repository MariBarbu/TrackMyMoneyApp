using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinApp.Services;
using XamarinApp.Views.Month;

namespace XamarinApp.ViewModels.Month
{
    public class DefaultScreenViewModel : BaseViewModel
    {
        private readonly IMonthService _monthService;
        public decimal budget;
        public decimal spendings;
        public decimal economies;
        public decimal percent;
        public DefaultScreenViewModel(IMonthService monthService)
        {
            _monthService = monthService;
            UpdateBudgetCommand = new Command(async () => await GoToUpdateBudget());
            Title= "My Money";
        }
        public async void GetDefaultScreen()
        {
            try
            {
                var screen = await _monthService.GetDefaultScreen();
                budget = screen.Budget;
                economies = screen.Economies;
                spendings = screen.Spendings;
                percent = spendings/budget;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public decimal Budget
        {
            get => budget;
            set => SetProperty(ref budget, value);
        }
        public decimal Spendings
        {
            get => spendings;
            set => SetProperty(ref spendings, value);
        }
        public decimal Economies
        {
            get => economies;
            set => SetProperty(ref economies, value);
        }
        public decimal Percent
        {
            get => percent;
            set => SetProperty(ref percent, value);
        }

        private async Task GoToUpdateBudget()
            => await Shell.Current.GoToAsync(nameof(UpdateBudgetPage));

        public ICommand UpdateBudgetCommand { get; }
    }
}
