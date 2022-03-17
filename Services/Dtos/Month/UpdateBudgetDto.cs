using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Services.Dtos.Month
{
    public class UpdateBudgetDto
    {
        [Required]
        [Range(0, 999999, ErrorMessage = ErrorService.InvalidValue), Column(TypeName = "decimal(18,4)")]
        public decimal Budget { get; set; }
    }
}
