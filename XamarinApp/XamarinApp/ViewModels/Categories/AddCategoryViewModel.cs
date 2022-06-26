using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinApp.Models;
using XamarinApp.Services;

namespace XamarinApp.ViewModels.Categories
{
    public class AddCategoryViewModel : BaseViewModel
    {
        private readonly ICategoryService _categoryService;
        private string name;
        private bool isValid;
       
        public AddCategoryViewModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
            Title = "Add Category";
            SaveCategoryCommand = new Command(async () => await SaveCategory());
            CancelCommand = new Command(OnCancel);
        }

        private async Task SaveCategory()
        {
            try
            {
                var category = new AddCategory
                {
                    Name=name
                };

                var result = await _categoryService.AddCategoryAsync(category);
                if (!result)
                {
                    await App.Current.MainPage.DisplayAlert("Something went wrong", "Category not added", "Ok");
                    return;
                }
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public bool IsValid
        {
            get => isValid;
            set
            {
                isValid = value;
                OnPropertyChanged(nameof(IsValid));
            }
        }


        public ICommand SaveCategoryCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
