using System;
using System.Collections.Generic;
using System.Text;

namespace Sello.Application.DTO
{
    public class ProductResponseDto
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal StockQuantity { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public double AverageRating { get; set; }
        public int ReviewCount { get; set; }
    }
}
