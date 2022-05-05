using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
      
        public string email { get; set; }
        public string password { get; set; }
        

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

        [Required]
        [MaxLength(100)]
        public string Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Field should not be empty")]
        [MaxLength(100)]
        [MinLength(8, ErrorMessage = "Password Too Short")]
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
