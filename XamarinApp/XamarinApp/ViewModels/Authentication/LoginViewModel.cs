using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinApp.Helpers;
using XamarinApp.Models;
using XamarinApp.Services;
using XamarinApp.Views;

namespace XamarinApp.ViewModels.Authentication
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly IAuthService _authService;
      
        public string Email { get; set; }
        public string Password { get; set; }
        

        public LoginViewModel(IAuthService authService)
        {
            _authService = authService;
            Settings.Username = Email;
            Settings.Password = Password;

        }
        public ICommand LoginCommand
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        var user = new Login
                        {
                            
                            Email = Email,
                            Password = Password
                        };

                        var accessToken = await _authService.LoginAsync(user);
                        Settings.AccessToken = accessToken;
                        await Shell.Current.GoToAsync("//WishesPage");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                });
            }
        }
    }
}
