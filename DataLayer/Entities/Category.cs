using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public Guid MoneyUserId { get; set; }
        public MoneyUser MoneyUser { get; set; }

        public virtual IList<Spending> Spendings { get; set; } = new List<Spending>();
    }
}
