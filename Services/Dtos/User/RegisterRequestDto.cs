﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Dtos.User
{
    public class RegisterRequestDto
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime BirthDate { get; set; }
    }
}