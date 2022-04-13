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
        //private decimal budget;
        private decimal currentBudget;

        public UpdateBudgetViewModel(IMonthService monthService)
        {
            _monthService = monthService;

            SaveBudgetCommand = new Command(async () => await SaveBudget());
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
            await Shell.Current.GoToAsync("CategoriesPage");
            
        }

        //public decimal Budget
        //{
        //    get => budget;
        //    set
        //    {
        //        budget = value;
        //        OnPropertyChanged(nameof(Budget));
        //    }
        //}
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

        public ICommand SaveBudgetCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

    }
}
