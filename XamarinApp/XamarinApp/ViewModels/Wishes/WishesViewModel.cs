using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinApp.Models;
using XamarinApp.Services;

namespace XamarinApp.ViewModels.Wishes
{
    public class WishesViewModel : BaseViewModel
    {
        private ObservableCollection<Wish> wishes;
        //private Wish selectedWish;
        private readonly IWishService _wishService;

        public WishesViewModel(IWishService wishService)
        {
            _wishService = wishService;

            Wishes = new ObservableCollection<Wish>();

            DeleteWishCommand = new Command<Wish>(async b => await DeleteWish(b));

            //AddNewWishCommand = new Command(async () => await GoToAddWishView());
        }

        private async Task DeleteWish(Wish b)
        {
            await _wishService.DeleteWish(b);

            PopulateBooks();
        }

        //private async Task GoToAddWishView()
        //    => await Shell.Current.GoToAsync(nameof(AddWish));

        public async void PopulateBooks()
        {
            try
            {
                Wishes.Clear();

                var wishes = await _wishService.GetWishes();
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

        //async void OnBookSelected(Wish wish)
        //{
        //    if (wish == null)
        //        return;

        //    await Shell.Current.GoToAsync($"{nameof(WishDetails)}?{nameof(WishDetailsViewModel.BookId)}={book.Id}");
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

        //public Book SelectedBook
        //{
        //    get => selectedBook;
        //    set
        //    {
        //        if (selectedBook != value)
        //        {
        //            selectedBook = value;

        //            OnBookSelected(SelectedBook);
        //        }
        //    }
        //}

        public ICommand DeleteWishCommand { get; }

        //public ICommand AddNewWishCommand { get; }
    }
}