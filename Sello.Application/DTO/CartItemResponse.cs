using System;
using System.Collections.Generic;
using System.Text;

namespace Sello.Application.DTO
{
    public class CartItemResponse
    {
        public Guid CartItemId { get; set; }
        public Guid ProductId { get; set; }
        public string? ProductName { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
    }
}
