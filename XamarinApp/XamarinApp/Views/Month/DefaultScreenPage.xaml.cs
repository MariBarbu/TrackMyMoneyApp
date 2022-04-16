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
    public partial class DefaultScreenPage : ContentPage
    {
        private readonly DefaultScreenViewModel _defaultScreenViewModel;
        public DefaultScreenPage()
        {
            InitializeComponent();
            _defaultScreenViewModel = Startup.Resolve<DefaultScreenViewModel>();
            BindingContext = _defaultScreenViewModel;
        }
        protected override void OnAppearing()
        {
            _defaultScreenViewModel?.GetDefaultScreen();
        }
    }
}