using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Entities
{
    public class Spending : BaseEntity
    {
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public Guid MonthId { get; set; }
        public Month Month { get; set; }
        public decimal Cost { get; set; }
        public string Details { get; set; }
    }
}
