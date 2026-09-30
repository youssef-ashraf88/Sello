using System;
using System.Collections.Generic;
using System.Text;

namespace Sello.Domain.Entities
{
    public class OrderItem
    {
        public Guid Id { get; set; }

        public Guid OrderId { get; set; }

        public Guid ProductId { get; set; }

        public decimal Quantity { get; set; }

        public decimal UnitPriceSnapshot { get; set; }

        public string? ProductNameSnapshot { get; set; }

        //navigation Property

        public Order? Order { get; set; }

        public Product? Product { get; set; }
    }
}
