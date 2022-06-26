using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinApp.Models;
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
                if (screen.Budget == 0)
                {
                    var value = await GetValue();
                    if (value <= 0)
                    {
                        await App.Current.MainPage.DisplayAlert("Negative Value", "Please insert a positive value.", "Ok");
                        value = await GetValue();
                    }

                    var newBudget = new UpdateBudget
                    {
                        Budget = value
                    };
                    var result = await _monthService.UpdateBudget(newBudget);
                    if (result == "OK")
                        GetDefaultScreen();
                }
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

        private async Task<int> GetValue()
        {
            var response = await App.Current.MainPage.DisplayPromptAsync("It's a new month, so you need to set your budget.",
                    "How much do you plan to spend this month?", initialValue: "100", maxLength: 10, keyboard: Keyboard.Numeric);
            var value = Convert.ToInt32(response);
            return value;
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
