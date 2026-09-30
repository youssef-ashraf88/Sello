using Sello.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sello.Application.DTO
{
    public class OrderHistoryResponseDto
    {
        public Guid OrderId { get; set; }
        public OrderStatus Status { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
