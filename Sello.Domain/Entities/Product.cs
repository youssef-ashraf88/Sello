using System;
using System.Collections.Generic;
using System.Text;

namespace Sello.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; set; }

        public Guid CategoryId { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public decimal StockQuantity { get; set; }

        public decimal Price { get; set; }

        public bool IsActive { get; set; } = true;

        //navigation properties

        public Category? Category { get; set; }

        public IEnumerable<CartItem>? CartItems { get; set; }

        public IEnumerable<OrderItem>? OrderItems { get; set; }

        public IEnumerable<Review>? Reviews { get; set; }
    }
}
