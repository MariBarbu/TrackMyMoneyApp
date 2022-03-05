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
    }
}
