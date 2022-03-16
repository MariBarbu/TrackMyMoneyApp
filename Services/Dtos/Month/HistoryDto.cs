using Services.Dtos.Spending;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Dtos.Month
{
    public class HistoryDto
    {
        public decimal Budget { get; set; }
        public decimal Economies { get; set; }
        public decimal TotalSpent { get; set; }
        public IList<GetSpendingDto> Spendings { get; set; } = new List<GetSpendingDto>();
    }
}