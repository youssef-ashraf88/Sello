using System;
using System.Collections.Generic;
using System.Text;

namespace Sello.Application.DTO
{
    public class CreateShippingAddressDto
    {
        public string? FullName { get; set; }
        public string? AddressLine { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
        public string? PhoneNumber { get; set; }
        public bool IsDefault { get; set; } 
    }
}
