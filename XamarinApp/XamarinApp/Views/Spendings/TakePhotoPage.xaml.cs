using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinApp.Services;
using XamarinApp.ViewModels.Spendings;

namespace XamarinApp.Views.Spendings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TakePhotoPage : ContentPage
    {
        private readonly ImagesViewModel _imagesViewModel;
        public TakePhotoPage()
        {
            InitializeComponent();
            _imagesViewModel = Startup.Resolve<ImagesViewModel>();
            BindingContext = _imagesViewModel;
        }

        private async void CameraButton_Clicked(object sender, EventArgs e)
        {
            
            var photo = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions
            {
                Title = "Please take a photo"
            });
            if (photo == null)
                return;
            
            var stream = await photo.OpenReadAsync();
            PhotoImage.Source = ImageSource.FromStream(() => stream);
            MemoryStream ms = new MemoryStream();
            stream.CopyTo(ms);
            _imagesViewModel.SavePicture(ms.ToArray());
        }
        private async void PickButton_Clicked(object sender, EventArgs e)
        {

            var photo = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
            {
                Title = "Please pick a photo"
            });
            if (photo == null)
                return ;
           
            var stream = await photo.OpenReadAsync();
            PhotoImage.Source = ImageSource.FromStream(() => stream);
            MemoryStream ms = new MemoryStream();
            stream.CopyTo(ms);
            _imagesViewModel.SavePicture(ms.ToArray());
        }
        
    }
    
}