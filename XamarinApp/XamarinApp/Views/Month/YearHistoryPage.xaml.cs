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
    public partial class YearHistoryPage : ContentPage
    {
        private readonly YearHistoryViewModel _yearHistoryViewModel;
        public YearHistoryPage()
        {
            InitializeComponent();
            _yearHistoryViewModel = Startup.Resolve<YearHistoryViewModel>();
            BindingContext = _yearHistoryViewModel;
        }
        protected override void OnAppearing()
        {

              _yearHistoryViewModel?.GetHistory();
            
        }
    }
}