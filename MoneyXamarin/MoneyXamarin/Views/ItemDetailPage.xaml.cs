using MoneyXamarin.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace MoneyXamarin.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}