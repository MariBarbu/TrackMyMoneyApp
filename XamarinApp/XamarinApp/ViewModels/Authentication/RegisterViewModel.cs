using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinApp.Helpers;
using XamarinApp.Models;
using XamarinApp.Services;

namespace XamarinApp.ViewModels.Authentication
{
    public class RegisterViewModel : BaseViewModel
    {
        private readonly IAuthService _authService;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public DateTime BirthDate { get; set; }

        public RegisterViewModel(IAuthService authService)
        {
            _authService = authService;

        }
        public ICommand RegisterCommand
        {
            get {
                return new Command(async () =>
          {
              try
              {
                  var user = new Register
                  {
                    FirstName = FirstName,
                    LastName = LastName,
                    Email = Email,
                    Password = Password,
                    ConfirmPassword = ConfirmPassword,
                    BirthDate = BirthDate
                  };

                  Settings.Username = Email;
                  Settings.Password = Password;

                  await _authService.RegisterAsync(user);

                  await Shell.Current.GoToAsync("//LoginPage");
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
