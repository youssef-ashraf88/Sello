using System;
using System.Collections.Generic;
using System.Text;

namespace Sello.Application.DTO
{
    public class CartItemAddRequestDto
    {
        public Guid ProductId { get; set; }
        public decimal Quantity { get; set; }
    }
}
