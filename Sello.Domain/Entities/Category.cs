using System;
using System.Collections.Generic;
using System.Text;

namespace Sello.Domain.Entities
{
    public class Category
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        //navigation properties

        public  IEnumerable<Product>? Products { get; set; }
    }
}
