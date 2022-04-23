using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinApp.Models
{
    public class History
    {
        public decimal Budget { get; set; }
        public decimal Economies { get; set; }
        public decimal TotalSpent { get; set; }
        public IList<Spending> Spendings { get; set; } = new List<Spending>();
    }
}
