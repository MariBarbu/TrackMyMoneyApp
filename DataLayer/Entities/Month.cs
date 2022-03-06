using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Entities
{
    public class Month : BaseEntity
    {
        public int Year { get; set; }
        public int MonthOfYear { get; set; }
        public decimal Budget { get; set; }
        public decimal Economies { get; set; }
        public Guid MoneyUserId { get; set; }
        public MoneyUser MoneyUser { get; set; }

        public virtual IList<Spending> Spendings { get; set; } = new List<Spending>();

    }
}
