using System;
using System.Collections.Generic;
using System.Text;

namespace Sello.Application.DTO
{
    public class CategoryResponseDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
