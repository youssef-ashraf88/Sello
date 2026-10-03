using System;
using System.Collections.Generic;
using System.Text;

namespace Sello.Application.DTO
{
    public class UserResponseDto
    {
            public Guid Id { get; set; }
            public string? PersonName { get; set; }
            public string? Email { get; set; }
            public string? PhoneNumber { get; set; }
    }
}
