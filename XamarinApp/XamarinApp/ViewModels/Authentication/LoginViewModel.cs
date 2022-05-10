using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
      
        private string email { get; set; }
        private string password { get; set; }
        private bool isEmailValid { get; set; }
        private bool isPasswordValid { get; set; }

        public LoginViewModel(IAuthService authService)
        {
            _authService = authService;
            Settings.Username = email;
            Settings.Password = password;
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
                            
                            Email = email,
                            Password = password
                        };

                        var accessToken = await _authService.LoginAsync(user);
                        if(accessToken == null)
                        {
                            await App.Current.MainPage.DisplayAlert("Something went wrong", "Email or password incorrect, please try again", "Ok");
                            return;
                        }
                        Settings.AccessToken = accessToken;

                        await Application.Current.MainPage.Navigation.PopModalAsync();
                        Application.Current.MainPage = new AppShell();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                });
            }
        }
        
        public bool IsEmailValid
        {
            get => isEmailValid;
            set
            {
                isEmailValid = value;
                OnPropertyChanged(nameof(IsEmailValid));
            }
        }
        public bool IsPasswordValid
        {
            get => isPasswordValid;
            set
            {
                isPasswordValid = value;
                OnPropertyChanged(nameof(IsPasswordValid));
            }
        }

        public string Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

    }
}
