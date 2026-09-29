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

        public Guid ShippingAddressId { get; set; }

        public OrderStatus Status { get; set; }

        public decimal TotalAmount { get; set; }

        public DateTime CreatedAt { get; set; }

        public Guid UserId { get; set; }

        //navigation Proderties

        public ApplicationUser? User { get; set; }

        public ICollection<OrderItem>? OrderItems { get; set; }

        public ShippingAddress? ShippingAddress { get; set; }
    }
}
