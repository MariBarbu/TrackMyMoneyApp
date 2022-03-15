using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Services.Dtos.Month
{
    public class UpdateBudgetDto
    {
        [Required]
        [Range(1, 9999999999999999.99, ErrorMessage = ErrorService.NegativeValue)]
        public decimal Budget { get; set; }
    }
}
