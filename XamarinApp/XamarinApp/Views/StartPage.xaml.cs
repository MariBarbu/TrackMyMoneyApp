using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinApp.Views.Authentication;

namespace XamarinApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartPage : ContentPage
    {
        public StartPage()
        {
            InitializeComponent();
        }


        private async void OnLoginClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new LoginPage());

        }
        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new RegisterPage());
        }
    }
}