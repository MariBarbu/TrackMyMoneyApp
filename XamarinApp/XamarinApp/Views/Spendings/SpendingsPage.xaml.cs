using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinApp.ViewModels.Spendings;

namespace XamarinApp.Views.Spendings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SpendingsPage : ContentPage
    {
        public readonly SpendingsViewModel _spendingsViewModel;
        public SpendingsPage()
        {
            InitializeComponent();
            _spendingsViewModel = Startup.Resolve<SpendingsViewModel>();
            BindingContext = _spendingsViewModel;
        }
        protected override void OnAppearing()
        {
            _spendingsViewModel?.PopulateSpendings();
        }
    }
}