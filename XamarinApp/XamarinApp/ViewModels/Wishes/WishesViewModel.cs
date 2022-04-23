using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinApp.Models;
using XamarinApp.Services;
using XamarinApp.Views;
using XamarinApp.Views.Wishes;

namespace XamarinApp.ViewModels.Wishes
{
    public class WishesViewModel : BaseViewModel
    {
        private ObservableCollection<Wish> wishes;
        private readonly IWishService _wishService;

        public WishesViewModel(IWishService wishService)
        {
            _wishService = wishService;
            Title = "Wishes";
            Wishes = new ObservableCollection<Wish>();

            DeleteWishCommand = new Command<Wish>(async b =>
            {
                await DeleteWish(b);
            }); 
            SwitchWishCommand = new Command<Wish>(async b =>
            {
                await SwitchStatus(b);
            });

            AddNewWishCommand = new Command(async () => await GoToAddWishView());
           
        }

        private async Task DeleteWish(Wish wish)
        {
           
            await _wishService.DeleteWish(wish); 

            PopulateWishes();
        }

        private async Task GoToAddWishView()
            => await Shell.Current.GoToAsync(nameof(AddWishPage));

        public async void PopulateWishes()
        {
            try
            {
                Wishes.Clear();

                var wishes = await _wishService.GetWishes().ConfigureAwait(false);
                foreach (var wish in wishes)
                {
                    Wishes.Add(wish);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task SwitchStatus(Wish wish)
        {
            
                var result = await _wishService.ChangeStatus(wish);
                if (result!= "OK")
                    await App.Current.MainPage.DisplayAlert("Something went wrong!", "Not enough money in your economies!", "OK");
            PopulateWishes();
        }

        //async void OnWishSelected(Wish wish)
        //{
        //    if (wish == null)
        //        return;
        //    //await Shell.Current.GoToAsync($"{nameof(WishDetails)}?{nameof(WishDetailsViewModel.BookId)}={wish.Id}");
        //}

        public ObservableCollection<Wish> Wishes
        {
            get => wishes;
            set
            {
                wishes = value;
                OnPropertyChanged(nameof(Wishes));
            }
        }

        //public Wish SelectedWish
        //{
        //    get => selectedWish;
        //    set
        //    {
        //        if (selectedWish != value)
        //        {
        //            selectedWish = value;

        //            OnWishSelected(SelectedWish);
        //        }
        //    }
        //}

        public ICommand DeleteWishCommand { get; }
        public ICommand SwitchWishCommand { get; }

        public ICommand AddNewWishCommand { get; }
    }
}