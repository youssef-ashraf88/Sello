using System;
using System.Collections.Generic;
using System.Text;

namespace Sello.Application.DTO
{
    public class CartResponseDto
    {
        public Guid CartId { get; set; }
        public Guid UserId { get; set; }
        public IEnumerable<CartItemResponse> CartItems { get; set; } = new List<CartItemResponse>();
        public decimal Total { get; set; }
    }
}
