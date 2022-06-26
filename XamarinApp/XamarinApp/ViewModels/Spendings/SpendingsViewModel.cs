using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinApp.Models;
using XamarinApp.Services;
using XamarinApp.Views.Spendings;

namespace XamarinApp.ViewModels.Spendings
{
    [QueryProperty(nameof(CategoryId), nameof(CategoryId))]
    public class SpendingsViewModel : BaseViewModel
    {
        private ObservableCollection<Spending> spendings;
        private string categoryName;
        private string categoryId;
        private readonly ISpendingService _spendingService;

        public SpendingsViewModel(ISpendingService spendingService)
        {
            _spendingService = spendingService;
            Title = "Spendings";
            Spendings = new ObservableCollection<Spending>();

            DeleteSpendingCommand = new Command<Spending>(async b => await DeleteSpending(b));

           

        }

        private async Task DeleteSpending(Spending b)
        {
            var result = await _spendingService.DeleteSpending(b.Id);
            if (!result)
            {
                await App.Current.MainPage.DisplayAlert("Something went wrong!", "Spending expired!", "OK");
            }
            PopulateSpendings();
        }

        

        public async void PopulateSpendings()
        {
            try
            {
                Spendings.Clear();

                var categoryWishSpendings = await _spendingService.GetSpendings(Guid.Parse(categoryId)).ConfigureAwait(false);
                Spendings = categoryWishSpendings.Spendings;
                CategoryName = categoryWishSpendings.CategoryName;
                CategoryId = categoryWishSpendings.CategoryId.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        

        public ObservableCollection<Spending> Spendings
        {
            get => spendings;
            set
            {
                spendings = value;
                OnPropertyChanged(nameof(Spendings));
            }
        }

        public string CategoryId
        {
            get => categoryId;
            set => SetProperty(ref categoryId, value);
        }


        public string CategoryName
        {
            get => categoryName;
            set => SetProperty(ref categoryName, value);
        }



        public ICommand DeleteSpendingCommand { get; }

        
    }
}
