
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sello.Application.DTO;
using Sello.Application.Helpers;
using Sello.Application.ServiceContracts;
using Sello.Domain.Entities.Identity;
using Sello.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.WebRequestMethods;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Sello.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IJwtService _jwtService;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager, IJwtService jwtService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _jwtService = jwtService;
        }

        public async Task<Result<RegisterResponse>> Register(RegisterDto input)
        {
            var check = await _userManager.FindByEmailAsync(input.Email!);
            if (check is not null)
                return Result<RegisterResponse>.Fail("Registration failed, Please check your data and try again.");

            var user = new ApplicationUser()
            {
                Email = input.Email,
                PersonName = input.Username,
                PhoneNumber = input.PhoneNumber,
                UserName = input.Email
            };

            var result = await _userManager.CreateAsync(user, input.Password!);
            if (!result.Succeeded)
            {
                var errors = string.Join(",", result.Errors.Select(e => e.Description));
                return Result<RegisterResponse>.Fail(errors);
            }

            var role = await _userManager.AddToRoleAsync(user, RoleOptions.Customer.ToString());
            if (!role.Succeeded)
            {
                var errors = string.Join(",", role.Errors.Select(e => e.Description));
                return Result<RegisterResponse>.Fail(errors);
            }

            var registerResponse = new RegisterResponse()
            {
                Email = user.Email,
                PersonName = user.PersonName
            };

            return Result<RegisterResponse>.Success(registerResponse);
        }

        public async Task<Result<LoginResponse>> Login(LoginDto input)
        {
            var user = await _userManager.FindByEmailAsync(input.Email!);
            if (user == null)
                return Result<LoginResponse>.Fail("Email or password is incorrect.");

            var passwordCheck = await _userManager.CheckPasswordAsync(user, input.Password!);
            if (!passwordCheck)
                return Result<LoginResponse>.Fail("Email or password is incorrect.");

            var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault() ?? RoleOptions.Customer.ToString();

            var loginResponse = _jwtService.CreateJwtToken(user, role);

            return Result<LoginResponse>.Success(loginResponse);
        }

        
    }
}
