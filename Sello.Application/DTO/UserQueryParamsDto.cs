using System;
using System.Collections.Generic;
using System.Text;

namespace Sello.Application.DTO
{
    public class UserQueryParamsDto
    {
        public string? Search { get; set; }
        public int PageNumber { get; set; } 
        public int PageSize { get; set; }
    }
}
