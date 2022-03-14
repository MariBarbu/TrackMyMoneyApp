using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Dtos.Spending
{
    public class AddSpendingDto
    {
        public Guid? Id { get; set; }
        public Guid CategoryId { get; set; }
        public decimal Cost { get; set; }
        public string Details { get; set; }
    }
}
