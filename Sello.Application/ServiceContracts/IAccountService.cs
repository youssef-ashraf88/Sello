using Sello.Application.DTO;
using Sello.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sello.Application.ServiceContracts
{
    public interface IAccountService
    {
        Task<Result<RegisterResponse>> Register(RegisterDto input);

        Task<Result<LoginResponse>> Login(LoginDto input);
    }
}
