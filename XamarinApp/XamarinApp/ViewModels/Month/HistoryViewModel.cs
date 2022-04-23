using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinApp.Services;
using XamarinApp.Views.Month;

namespace XamarinApp.ViewModels.Month
{
    public class HistoryViewModel : BaseViewModel
    {
        private readonly IMonthService _monthService;
        private ObservableCollection<int> years;
        private ObservableCollection<int> months;
        public int year;
        public int month;
        public HistoryViewModel(IMonthService monthService)
        {
            _monthService = monthService;
            Years = new ObservableCollection<int>();
            Months = new ObservableCollection<int>();
            Title= "History";
            FindCommand = new Command(async () => await GoToYearHistory());
        }
        private async Task GoToYearHistory()
        {
            if (year == 0)
                return;
            if (month == 0)
                await Shell.Current.GoToAsync($"{nameof(YearHistoryPage)}?{nameof(YearHistoryViewModel.Year)}={year}");
            else
                await Shell.Current.GoToAsync($"{nameof(MonthHistoryPage)}?{nameof(MonthHistoryViewModel.Year)}={year}&{nameof(MonthHistoryViewModel.Month)}={month}");
        }
            
        public async void GetYearsAndMonths()
        {

            Years.Clear();
            Months.Clear();

            var years = await _monthService.GetYears();
            //var years =  _monthService.GetY();
            foreach (var year in years)
            {
                Years.Add(year);
            }

            for (int i = 0; i < 12; i++)
            {
                Months.Add(i);
                //years.Add(i+2022);
            }
           

        }
        
        public ObservableCollection<int> Years
        {
            get => years;
            set
            {
                years = value;
                OnPropertyChanged(nameof(Years));
            }
        }

        public ObservableCollection<int> Months
        {
            get => months;
            set
            {
                months = value;
                OnPropertyChanged(nameof(Months));
            }
        }


        public int Year
        {
            get => year;
            set
            {
                year = value;
                OnPropertyChanged(nameof(Year));
            }
        }
        public int Month
        {
            get => month;
            set
            {
                month = value;
                OnPropertyChanged(nameof(Month));
            }
        }

        public ICommand FindCommand { get; }
    }
}
