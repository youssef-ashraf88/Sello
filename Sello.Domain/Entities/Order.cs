using Sello.Domain.Entities.Identity;
using Sello.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;

namespace Sello.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; set; }

        public OrderStatus Status { get; set; }

        public decimal TotalAmount { get; set; }

        public DateTime CreatedAt { get; set; }

        public Guid UserId { get; set; }

        //shipping address snapshot
        public string? FullName { get; set; }
        public string? AddressLine { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
        public string? PhoneNumber { get; set; }

        //navigation Proderties

        public ApplicationUser? User { get; set; }

        public ICollection<OrderItem>? OrderItems { get; set; }
    }
}
