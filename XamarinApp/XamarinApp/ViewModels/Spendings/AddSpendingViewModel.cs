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

                var categories = await _categoryService.GetCategories().ConfigureAwait(false);
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

                await _spendingService.AddSpending(spending);

                //await Shell.Current.GoToAsync($"{nameof(SpendingsPage)}?{nameof(SpendingsViewModel.CategoryId)}={categoryId}");
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
        public ICommand SaveSpendingCommand { get; }
        public Command CancelCommand { get; }
        

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
       
    }
}
