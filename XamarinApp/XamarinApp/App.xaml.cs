using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinApp.Helpers;
using XamarinApp.Services;
using XamarinApp.Views;

namespace XamarinApp
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            Startup.ConfigureServices();
            DependencyService.Register<MockDataStore>();
            if (!String.IsNullOrEmpty(Settings.AccessToken))
                MainPage = new AppShell();
            else
                MainPage = new StartPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
