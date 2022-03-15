using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Services.Dtos.Spending
{
    public class AddSpendingDto
    {
        public Guid? Id { get; set; }
        [Required]
        public Guid CategoryId { get; set; }
        [Required]
        [Range(0, 999999, ErrorMessage = ErrorService.InvalidValue), Column(TypeName = "decimal(18,4)")]
        public decimal Cost { get; set; }
        [MaxLength(10000, ErrorMessage = ErrorService.DescriptionTooLong)]
        public string Details { get; set; }
    }
}
