using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Dtos.Month
{
    public class GetDefaultScreenDto
    {
        public Guid MonthId { get; set; }
        public decimal Budget { get; set; }
        public decimal Spendings { get; set; }
        public decimal Economies { get; set; }

    }
}
