using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Dtos.Profile
{
    public class GetProfileDto
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
    }
}
