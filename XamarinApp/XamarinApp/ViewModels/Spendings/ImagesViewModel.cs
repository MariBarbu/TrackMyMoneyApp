using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinApp.Models;
using XamarinApp.Services;
using XamarinApp.Views.Spendings;

namespace XamarinApp.ViewModels.Spendings
{
    public class ImagesViewModel : BaseViewModel
    {
        private readonly ISpendingService _spendingService;

        public ImagesViewModel(ISpendingService spendingService)
        {
            _spendingService = spendingService;

        }

        public async Task SavePictureAsync(byte[] array, string fileName)
        {
            var picture = new Picture
            {
                Image = array,
                FileName = fileName
            };
           var spending  = await _spendingService.UploadPicture(picture);
            var response = await App.Current.MainPage.DisplayAlert("You are about to add this spending to your Various Category",
                    $"Cost: {spending.Cost} \n Details: {spending.Details}", "Yes", "No");
            if (response)
            {
                var result = await _spendingService.AddSpending(spending);
                if (!result)
                {
                    await App.Current.MainPage.DisplayAlert("Something went wrong", "Spending not added", "Ok");
                    return;
                }

            }
            await Shell.Current.GoToAsync("..");

        }
    }
}
