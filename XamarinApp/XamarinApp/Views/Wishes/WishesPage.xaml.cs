using Xamarin.Forms;
using XamarinApp.ViewModels.Wishes;

namespace XamarinApp.Views
{
    public partial class WishesPage : ContentPage
    {
        private readonly WishesViewModel _wishesViewModel;

            public WishesPage()
            {
                InitializeComponent();

                _wishesViewModel = Startup.Resolve<WishesViewModel>();
                BindingContext = _wishesViewModel;
            }

        protected override void OnAppearing()
        {
            _wishesViewModel?.PopulateBooks();
        }
    }
}