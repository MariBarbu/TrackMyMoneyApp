using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Dtos.User
{
    public class UserInformationDto
    {
        public string FirstName { get; set; } 
        public string LastName { get; set; }
        public string Email { get; set; }
        public string AccessToken { get; set; }
    }
}
