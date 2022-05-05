using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Services.Dtos.Profile
{
    public class EditProfileDto
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = ErrorService.NameIsRequired), MaxLength(100, ErrorMessage = ErrorService.NameTooLong)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = ErrorService.NameIsRequired), MaxLength(100, ErrorMessage = ErrorService.NameTooLong)]
        public string LastName { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
    }
}
