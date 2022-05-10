using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinApp.Models;
using XamarinApp.Services;
using XamarinApp.Views.Spendings;

namespace XamarinApp.ViewModels.Spendings
{
    public class AddSpendingViewModel : BaseViewModel
    {
        private ObservableCollection<GetCategories> categories;
        private readonly ISpendingService _spendingService;
        private readonly ICategoryService _categoryService;
        private decimal cost;
        private string details;
        private bool isCostValid;
        private bool isDetailsValid;
        private GetCategories category;

        public AddSpendingViewModel(ISpendingService spendingService, ICategoryService categoryService)
        {
            _spendingService = spendingService;
            _categoryService = categoryService;
            Categories = new ObservableCollection<GetCategories>();
            SaveSpendingCommand = new Command(async () => await SaveSpending());
            CancelCommand = new Command(OnCancel);
           
        }

        public async void PopulateCategories()
        {
            try
            {
                Categories.Clear();

                var categories = await _categoryService.GetCategories();
                foreach (var category in categories)
                {
                    Categories.Add(category);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public ObservableCollection<GetCategories> Categories
        {
            get => categories;
            set
            {
                categories = value;
                OnPropertyChanged(nameof(Categories));
            }
        }
        private async Task SaveSpending()
        {
            try
            {
                var spending = new AddSpending
                {
                    Cost = cost,
                    Details = details,
                    CategoryId = category.Id
                };

                var result = await _spendingService.AddSpending(spending);
                if(!result)
                {
                    await App.Current.MainPage.DisplayAlert("Something went wrong", "Spending not added", "Ok");
                    return;
                }
                
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public decimal Cost
        {
            get => cost;
            set
            {
                cost = value;
                OnPropertyChanged(nameof(Cost));
            }
        }
        public string Details
        {
            get => details;
            set
            {
                details = value;
                OnPropertyChanged(nameof(Details));
            }
        }

        public GetCategories Category
        {
            get => category;
            set
            {
                category = value;
                OnPropertyChanged(nameof(Category));
            }
        }

        public bool IsCostValid
        {
            get => isCostValid;
            set
            {
                isCostValid = value;
                OnPropertyChanged(nameof(IsCostValid));
            }
        }
        public bool IsDetailsValid
        {
            get => isDetailsValid;
            set
            {
                isDetailsValid = value;
                OnPropertyChanged(nameof(IsDetailsValid));
            }
        }
        public ICommand SaveSpendingCommand { get; }
        public Command CancelCommand { get; }
        

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
       
    }
}
