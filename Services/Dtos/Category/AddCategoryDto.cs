using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Services.Dtos.Category
{
    public class AddCategoryDto
    {
        [Required(ErrorMessage = ErrorService.NameIsRequired), MaxLength(100, ErrorMessage = ErrorService.NameTooLong)]
        public string Name { get; set; }
    }
}
