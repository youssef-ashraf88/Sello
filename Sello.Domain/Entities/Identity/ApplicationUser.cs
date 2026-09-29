using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sello.Domain.Entities.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string? PersonName { get; set; }

        //navigation properties

        public Cart? Cart { get; set; }

        public IEnumerable<Order>? Orders { get; set; }

        public IEnumerable<Review>? Reviews { get; set; }

        public ICollection<ShippingAddress> ShippingAddresses { get; set; } = new List<ShippingAddress>();
    }
}
