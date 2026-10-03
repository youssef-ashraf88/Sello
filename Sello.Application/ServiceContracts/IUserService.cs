using Sello.Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sello.Application.ServiceContracts
{
    public interface IUserService
    {
        Task<PagedResultResponseDto<UserResponseDto>> GetAllUsers(UserQueryParamsDto queryParams);
    }
}
