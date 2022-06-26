using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Services.Dtos.User
{
    public class RegisterRequestDto
    {
        [Required]
        [MaxLength(100, ErrorMessage = ErrorService.NameTooLong)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = ErrorService.NameTooLong)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [MaxLength(100)]
        [MinLength(8, ErrorMessage = ErrorService.PasswordTooShort)]
        public string Password { get; set; }
      
        [Required]
        public DateTime BirthDate { get; set; }
    }
}
