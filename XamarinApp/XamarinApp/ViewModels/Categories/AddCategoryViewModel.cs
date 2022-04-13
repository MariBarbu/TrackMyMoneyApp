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

                await _categoryService.AddCategoryAsync(category);

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

        
        public ICommand SaveCategoryCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
