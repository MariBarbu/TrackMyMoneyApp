using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinApp.ViewModels.Categories;

namespace XamarinApp.Views.Categories
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoriesPage : ContentPage
    {
        private readonly CategoriesViewModel _categoriesViewModel;
        public CategoriesPage()
        {
            InitializeComponent();
            _categoriesViewModel = Startup.Resolve<CategoriesViewModel>();
            BindingContext = _categoriesViewModel;
        }

        protected override void OnAppearing()
        {
            _categoriesViewModel?.PopulateCategories();
        }
    }
}