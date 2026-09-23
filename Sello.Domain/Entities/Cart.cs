using Sello.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sello.Domain.Entities
{
    public class Cart
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        //navigation properties

        public ApplicationUser? User { get; set; }

        public IEnumerable<CartItem>? CartItems { get; set; }
    }
}
