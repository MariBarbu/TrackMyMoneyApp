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
    public partial class MonthHistoryPage : ContentPage
    {
        private readonly MonthHistoryViewModel _monthHistoryViewModel;
        public MonthHistoryPage()
        {
            InitializeComponent();
            _monthHistoryViewModel = Startup.Resolve<MonthHistoryViewModel>();
            BindingContext = _monthHistoryViewModel;
        }
        protected override void OnAppearing()
        {

            _monthHistoryViewModel?.GetHistory();

        }
    }
}