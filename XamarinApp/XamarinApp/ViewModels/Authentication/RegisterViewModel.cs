using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinApp.Helpers;
using XamarinApp.Models;
using XamarinApp.Services;
using XamarinApp.Views.Authentication;

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
        private bool isFirstNameValid { get; set; }
        private bool isLastNameValid { get; set; }
        private bool isEmailValid { get; set; }
        private bool isPasswordValid { get; set; }
        private bool isCPasswordValid { get; set; }
        public DateTime BirthDate { get; set; }
        private DateTime propertyMaximumDate = DateTime.UtcNow;
        private DateTime propertyMinimumDate = DateTime.UtcNow.AddYears(-120);

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

                  var result = await _authService.RegisterAsync(user);
                  if (result)
                  {
                      await App.Current.MainPage.DisplayAlert("User created", "Please go to login", "Ok");
                      await Application.Current.MainPage.Navigation.PopModalAsync();
                  }
                  else
                  {
                      await App.Current.MainPage.DisplayAlert("Something went wrong", "User not created", "Ok");
                  }
              }
              catch (Exception ex)
              {
                  Console.WriteLine(ex.Message);
              }
          });
            }
        }

        public bool IsFirstNameValid
        {
            get => isFirstNameValid;
            set
            {
                isFirstNameValid = value;
                OnPropertyChanged(nameof(IsFirstNameValid));
            }
        }

        public bool IsLastNameValid
        {
            get => isLastNameValid;
            set
            {
                isLastNameValid = value;
                OnPropertyChanged(nameof(IsLastNameValid));
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
        public bool IsCPasswordValid
        {
            get => isCPasswordValid;
            set
            {
                isCPasswordValid = value;
                OnPropertyChanged(nameof(IsPasswordValid));
            }
        }
        public DateTime PropertyMaximumDate { get => propertyMaximumDate; set => SetProperty(ref propertyMaximumDate, value); }

        

        public DateTime PropertyMinimumDate { get => propertyMinimumDate; set => SetProperty(ref propertyMinimumDate, value); }


    }
}
