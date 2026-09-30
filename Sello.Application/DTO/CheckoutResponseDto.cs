using Sello.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sello.Application.DTO
{
    public class CheckoutResponseDto
    {
        public Guid OrderId { get; set; }
        public OrderStatus Status { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; }
        public ShippingAddressResponseDto ShippingAddress { get; set; }
        public ICollection<OrderItemResponseDto> Items { get; set; }
    }
}
