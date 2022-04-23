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
    public partial class HistoryPage : ContentPage
    {
        private readonly HistoryViewModel _historyViewModel;
        public HistoryPage()
        {
            InitializeComponent();
            _historyViewModel = Startup.Resolve<HistoryViewModel>();
            BindingContext = _historyViewModel;
        }

        protected override void OnAppearing()
        {
           _historyViewModel?.GetYearsAndMonths();
           

        }
    }
}