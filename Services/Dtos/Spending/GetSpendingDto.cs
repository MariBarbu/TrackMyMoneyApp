﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Dtos.Spending
{
    public class GetSpendingDto
    {
        public Guid Id { get; set; }
        public decimal Cost { get; set; }
        public string Details { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
