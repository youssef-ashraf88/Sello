using System;
using System.Collections.Generic;
using System.Text;

namespace Sello.Application.DTO
{
    public class LoginResponse
    {
        public string? PersonName { get; set; }

        public string? Email { get; set; }

        public string? Role { get; set; }

        public string? Token { get; set; }

        public DateTime Expiration { get; set; }

        //public string? RefreshToken { get; set; }

        //public DateTime? RefreshTokenExpirationDateTime { get; set; }
    }
}
