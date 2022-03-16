using DataLayer.Entities.Eums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Services.Dtos.Wish
{
    public class AddWishDto
    {
        [Required]
        [MaxLength(100, ErrorMessage = ErrorService.NameTooLong)]
        public string Name { get; set; }
        [MaxLength(10000, ErrorMessage = ErrorService.DescriptionTooLong)]
        public string Description { get; set; }
        [Required]
        [Range(0, 999999, ErrorMessage = ErrorService.InvalidValue), Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }
    }
}
