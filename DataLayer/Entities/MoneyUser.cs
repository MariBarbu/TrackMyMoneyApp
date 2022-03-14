using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Entities
{
    public class MoneyUser : BaseEntity
    {
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
        public Guid UserId { get; set; }
        public AppUser User { get; set; }
        public decimal Economies { get; set; }

        public virtual IList<Wish> Wishes { get; set; } = new List<Wish>();
        public virtual IList<Month> Months { get; set; } = new List<Month>();
        public virtual IList<Category> Categories { get; set; } = new List<Category>();
    }
}
