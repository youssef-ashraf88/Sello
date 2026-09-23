using System;
using System.Collections.Generic;
using System.Text;

namespace Sello.Domain.Entities
{
    public class CartItem
    {
        public Guid Id { get; set; }
        
        public Guid CartId { get; set; }
        
        public Guid ProductId { get; set; }
        
        public decimal Quantity { get; set; }

        //navigation properties

        public Cart? Cart { get; set; }

        public Product? Product { get; set; }
    }
}
