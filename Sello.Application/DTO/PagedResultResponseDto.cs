using System;
using System.Collections.Generic;
using System.Text;

namespace Sello.Application.DTO
{
    public class PagedResultResponseDto<T>
    {
        public IEnumerable<T> Items { get; set; } = Enumerable.Empty<T>();
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
    }
}
