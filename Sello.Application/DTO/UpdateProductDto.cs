using System;
using System.Collections.Generic;
using System.Text;

namespace Sello.Application.DTO
{
    public class UpdateProductDto
    {
        public Guid CategoryId { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public decimal StockQuantity { get; set; }

        public decimal Price { get; set; }
    }
}
