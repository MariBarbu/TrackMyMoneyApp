using System;
using System.Collections.Generic;
using Xamarin.Forms;
using XamarinApp.Helpers;
using XamarinApp.ViewModels;
using XamarinApp.Views;
using XamarinApp.Views.Authentication;
using XamarinApp.Views.Wishes;

namespace XamarinApp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            Settings.AccessToken = null;
            await Navigation.PushModalAsync(new StartPage());
        }
    }
}
