using System;
using System.Collections.Generic;
using System.Text;

namespace Sello.Application.DTO
{
    public class ReviewResponseDto
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
