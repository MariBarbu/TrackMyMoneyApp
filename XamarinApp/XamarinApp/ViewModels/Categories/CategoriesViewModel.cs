using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinApp.Models;
using XamarinApp.Services;
using XamarinApp.ViewModels.Spendings;
using XamarinApp.Views.Categories;
using XamarinApp.Views.Spendings;

namespace XamarinApp.ViewModels.Categories
{
    public class CategoriesViewModel : BaseViewModel
    {
        private ObservableCollection<GetCategories> categories;
        private GetCategories selectedCategory;
        private readonly ICategoryService _categoryService;

        public CategoriesViewModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
            Title = "Categories";
            Categories = new ObservableCollection<GetCategories>();

            DeleteCategoryCommand = new Command<GetCategories>(async b => await DeleteCategory(b));

            AddNewCategoryCommand = new Command(async () => await GoToAddCategoryView());
            AddNewSpendingCommand = new Command(async () => await GoToAddSpendingView());
            TakePhotoCommand = new Command(async () => await TakePhoto());

        }

        private async Task DeleteCategory(GetCategories b)
        {
            var result = await _categoryService.DeleteCategory(b);
            if(!result) await App.Current.MainPage.DisplayAlert("Something went wrong", "Category not deleted", "Ok");

            PopulateCategories();
        }
        private async Task GoToAddSpendingView()
            => await Shell.Current.GoToAsync(nameof(AddSpendingPage));
        private async Task GoToAddCategoryView()
            => await Shell.Current.GoToAsync(nameof(AddCategoryPage));
        private async Task TakePhoto()
        {
            await Shell.Current.GoToAsync(nameof(TakePhotoPage));
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

        async void OnCategorySelected(GetCategories category)
        {
            if (category == null)
                return;
            
           await Shell.Current.GoToAsync($"{nameof(SpendingsPage)}?{nameof(SpendingsViewModel.CategoryId)}={category.Id}");
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

        public GetCategories SelectedCategory
        {
            get => selectedCategory;
            set
            {
                if (selectedCategory != value)
                {
                    selectedCategory = value;

                    OnCategorySelected(SelectedCategory);
                }
            }
        }

        public ICommand DeleteCategoryCommand { get; }

        public ICommand AddNewCategoryCommand { get; }
        public ICommand AddNewSpendingCommand { get; }
        public ICommand TakePhotoCommand { get; }
    }
}
