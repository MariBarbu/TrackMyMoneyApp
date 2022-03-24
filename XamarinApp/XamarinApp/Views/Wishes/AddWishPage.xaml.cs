using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinApp.ViewModels.Wishes;

namespace XamarinApp.Views.Wishes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddWishPage : ContentPage
    {
        public AddWishPage()
        {
            InitializeComponent();

            BindingContext = Startup.Resolve<AddWishViewModel>();
        }
    }
}