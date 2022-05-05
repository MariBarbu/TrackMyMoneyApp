using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinApp.Services;
using XamarinApp.Models;

namespace XamarinApp.ViewModels.Profile
{
    public class ProfileViewModel : BaseViewModel
    {
        private readonly IProfileService _profileService;
        private string email;
        private string lastName;
        private string firstName;
        private DateTime birthDate;
        public ProfileViewModel(IProfileService profileService)
        {
            _profileService = profileService;
            Title = "My Profile";
            SaveProfileCommand = new Command(async () => await SaveProfile());

        }

        private async Task SaveProfile()
        {
            var profileToSave = new XamarinApp.Models.Profile
            {
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                BirthDate = birthDate
            };

            var result = await _profileService.EditProfile(profileToSave);
            if(result!= "OK")
            {
                await App.Current.MainPage.DisplayAlert("Something went wrong!", result, "OK");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Profile Saved","Information updated", "OK");
            }
           
        }

        public async Task GetCurrentInformation()
        {
            try
            {
                var profile = await _profileService.GetProfile();
                Email = profile.Email;
                FirstName = profile.FirstName;
                LastName = profile.LastName;
                BirthDate = profile.BirthDate;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
        public string LastName
        {
            get => lastName;
            set
            {
                lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }
        public string FirstName
        {
            get => firstName;
            set
            {
                firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }
        public DateTime BirthDate
        {
            get => birthDate;
            set
            {
                birthDate = value;
                OnPropertyChanged(nameof(BirthDate));
            }
        }
        public ICommand SaveProfileCommand { get; }
    }
}
