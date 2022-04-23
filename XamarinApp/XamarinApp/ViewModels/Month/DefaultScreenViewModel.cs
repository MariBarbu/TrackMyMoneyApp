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
                Budget = screen.Budget;
                Economies = screen.Economies;
                Spendings = screen.Spendings;
                Percent = spendings/budget;

               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public decimal Budget
        {
            get => budget;
            set
            {
                budget = value;
                OnPropertyChanged(nameof(Budget)); 
            }

        }
        public decimal Spendings
        {
            get => spendings;
            set
            {
                spendings = value;
                OnPropertyChanged(nameof(Spendings));
            }
        }
        public decimal Economies
        {
            get => economies;
            set
            {
                economies = value;
                OnPropertyChanged(nameof(Economies));
            }
        }
        public decimal Percent
        {
            get => percent;
            set
            {
                percent = value;
                OnPropertyChanged(nameof(Percent));
            }
        }

        private async Task GoToUpdateBudget()
            => await Shell.Current.GoToAsync(nameof(UpdateBudgetPage));

        public ICommand UpdateBudgetCommand { get; }
    }
}
