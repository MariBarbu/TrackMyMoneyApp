using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace XamarinApp.Models
{
    public class GetSpendings
    {
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public ObservableCollection<Spending> Spendings { get; set; } = new ObservableCollection<Spending>();
    }

    public class Spending
    {
        public Guid Id { get; set; }
        public decimal Cost { get; set; }
        public string Details { get; set; }
        public string CreatedAt { get; set; }
    }
}
