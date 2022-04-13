using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinApp.ViewModels.Month;

namespace XamarinApp.Views.Month
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdateBudgetPage : ContentPage
    {
        private readonly UpdateBudgetViewModel _updateBudgetViewModel;
        public UpdateBudgetPage()
        {
            InitializeComponent();
            _updateBudgetViewModel = Startup.Resolve<UpdateBudgetViewModel>();
            BindingContext = _updateBudgetViewModel;
        }
        protected override void OnAppearing()
        {
            _updateBudgetViewModel?.GetCurrentBudget();
        }
    }
}