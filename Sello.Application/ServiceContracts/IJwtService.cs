using Sello.Application.DTO;
using Sello.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sello.Application.ServiceContracts
{
    public interface IJwtService
    {
        LoginResponse CreateJwtToken(ApplicationUser user, string? role);
    }
}
