using Sello.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sello.Domain.Entities
{
    public class Review
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public int Rating { get; set; }

        public string? Comment { get; set; }

        public DateTime CreatedAt { get; set; }

        public Guid UserId { get; set; }

        //navigation Property

        public ApplicationUser? User { get; set; }

        public Product? Product { get; set; }
    }
}
