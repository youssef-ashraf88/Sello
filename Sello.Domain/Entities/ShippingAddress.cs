using Sello.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sello.Domain.Entities
{
    public class ShippingAddress
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string? FullName { get; set; }

        public string? AddressLine { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public string? PostalCode { get; set; }

        public string? Country { get; set; }

        public string? PhoneNumber { get; set; }

        public bool IsDefault { get; set; } = false;

        //navigation Property

        public ApplicationUser? User { get; set; }
    }
}
