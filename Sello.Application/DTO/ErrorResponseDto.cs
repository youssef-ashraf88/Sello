using System;
using System.Collections.Generic;
using System.Text;

namespace Sello.Application.DTO
{
    public class ErrorResponseDto
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
    }
}
