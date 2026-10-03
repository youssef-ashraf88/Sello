using System;
using System.Collections.Generic;
using System.Text;

namespace Sello.Application.DTO
{
    public class CreateOrUpdateReviewDto
    {
        public int Rating { get; set; }
        public string? Comment { get; set; }
    }
}
