using System;
using System.Collections.Generic;
using System.Text;

namespace Sello.Application.DTO
{
    public class RegisterDto
    {
        
        public string? Username { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        public string? ConfirmPassword { get; set; }

        public string? PhoneNumber { get; set; }
    }
}
