using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinApp.Models;
using XamarinApp.Services;

namespace XamarinApp.ViewModels.Wishes
{
    public class AddWishViewModel : BaseViewModel
    {
        private readonly IWishService _wishService;
        private string name;
        private string description;
        private decimal price;
        private bool isNameValid;
        private bool isPriceValid;
        private bool isDescriptionValid;

        public AddWishViewModel(IWishService wishService)
        {
            _wishService = wishService;

            SaveWishCommand = new Command(async () => await SaveWish());
            CancelCommand = new Command(OnCancel);
        }

        private async Task SaveWish()
        {
            try
            {
                var wish = new Wish
                {
                   Name=name,
                   Description=description,
                   Price=price
                };

                var result = await _wishService.AddWish(wish);
                if (!result)
                {
                    await App.Current.MainPage.DisplayAlert("Something went wrong", "Wish not added", "Ok");
                    return;
                }
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        
        public string Description
        {
            get => description;
            set
            {
                description = value;
                OnPropertyChanged(nameof(Description));
            }
        }
        public decimal Price
        {
            get => price;
            set
            {
                price = value;
                OnPropertyChanged(nameof(Price));
            }
        }
        public bool IsPriceValid
        {
            get => isPriceValid;
            set
            {
                isPriceValid = value;
                OnPropertyChanged(nameof(IsPriceValid));
            }
        }
        public bool IsNameValid
        {
            get => isNameValid;
            set
            {
                isNameValid = value;
                OnPropertyChanged(nameof(IsNameValid));
            }
        }

        public bool IsDescriptionValid
        {
            get => isDescriptionValid;
            set
            {
                isDescriptionValid = value;
                OnPropertyChanged(nameof(IsDescriptionValid));
            }
        }

        public ICommand SaveWishCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
