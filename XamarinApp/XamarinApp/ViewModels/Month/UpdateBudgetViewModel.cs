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
    public class UpdateBudgetViewModel : BaseViewModel
    {
        private readonly IMonthService _monthService;
        private decimal currentBudget;
        private decimal economy;
        private bool isBudgetValid;
        private bool isEconomyValid;

        public UpdateBudgetViewModel(IMonthService monthService)
        {
            _monthService = monthService;
            Title = "Update your budget";
            SaveBudgetCommand = new Command(async () => await SaveBudget());
            SaveEconomyCommand = new Command(async () => await SaveEconomy());

            CancelCommand = new Command(OnCancel);
            CurrentBudget = new decimal();
        }

        private async Task SaveBudget()
        {
                var budgetToSave = new UpdateBudget
                {
                    Budget = currentBudget
                };

                var result = await _monthService.UpdateBudget(budgetToSave);
            if(result!= "OK")
                await App.Current.MainPage.DisplayAlert("Something went wrong!", result, "OK");
            await Shell.Current.GoToAsync("..");
            
        }
        private async Task SaveEconomy()
        {
            var economyToSave = new NewEconomy
            {
                Economy = economy
            };

            var result = await _monthService.AddEconomy(economyToSave);
            if (result!= "OK")
                await App.Current.MainPage.DisplayAlert("Something went wrong!", result, "OK");
            await Shell.Current.GoToAsync("..");
        }


        public async void GetCurrentBudget()
        {
            try
            {
                var currentBudget = await _monthService.GetBudget();
                CurrentBudget = currentBudget.Budget;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public decimal CurrentBudget
        {
            get => currentBudget;
            set
            {
                currentBudget = value;
                OnPropertyChanged(nameof(CurrentBudget));
            }
        }
        public decimal Economy
        {
            get => economy;
            set
            {
                economy = value;
                OnPropertyChanged(nameof(Economy));
            }
        }
        public bool IsEconomyValid
        {
            get => isEconomyValid;
            set
            {
                isEconomyValid = value;
                OnPropertyChanged(nameof(IsEconomyValid));
            }
        }
        public bool IsBudgetValid
        {
            get => isBudgetValid;
            set
            {
                isBudgetValid = value;
                OnPropertyChanged(nameof(IsBudgetValid));
            }
        }

        public ICommand SaveBudgetCommand { get; }
        public ICommand SaveEconomyCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

    }
}
