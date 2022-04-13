using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinApp.Models
{
    public class AddSpending
    {
        public decimal Cost { get; set; }
        public string Details { get; set; }
        public Guid CategoryId { get; set; }
    }
}
