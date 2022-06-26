using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinApp.ViewModels.Profile;

namespace XamarinApp.Views.Profile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public readonly ProfileViewModel _profileViewModel;
        public ProfilePage()
        {
            InitializeComponent();
            _profileViewModel = Startup.Resolve<ProfileViewModel>();
            BindingContext = _profileViewModel;
        }

        protected override void OnAppearing()
        {

            _profileViewModel?.GetCurrentInformation();

        }
    }
}