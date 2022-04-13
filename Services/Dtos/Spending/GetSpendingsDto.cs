using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Dtos.Spending
{
    public class GetSpendingsDto
    {
        public Guid CategoryId {get;set;}
        public string CategoryName { get; set; }
        public IList<GetSpendingDto> Spendings { get; set; } = new List<GetSpendingDto>();
    }
}
