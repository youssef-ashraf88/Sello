using System;
using System.Collections.Generic;
using System.Text;

namespace Sello.Domain.Entities
{
    public class ShippingAddress
    {
        public Guid Id { get; set; }

        public Guid OrderId { get; set; }

        public string? FullName { get; set; }

        public string? AddressLine { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public string? PostalCode { get; set; }

        public string? Country { get; set; }

        public string? PhoneNumber { get; set; }

        //navigation Property

        public IEnumerable<Order>? Orders { get; set; }
    }
}
