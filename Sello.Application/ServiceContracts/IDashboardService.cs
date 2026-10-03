using Sello.Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sello.Application.ServiceContracts
{
    public interface IDashboardService
    {
        Task<DashboardResponseDto> GetDashboardStatistics();
    }
}
