﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Dtos
{
    public class LoginRequestDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
