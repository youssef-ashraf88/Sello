using System;
using System.Collections.Generic;
using System.Text;

namespace Sello.Application.DTO
{
    public class DashboardResponseDto
    {
        public int TotalUsers { get; set; }
        public int TotalProducts { get; set; }
        public int TotalOrders { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}
