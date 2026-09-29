using AutoMapper;
using Microsoft.AspNetCore.Http;
using Sello.Application.DTO;
using Sello.Application.ServiceContracts;
using Sello.Domain.Entities;
using Sello.Domain.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Sello.Application.Services
{
    public class ShippingAddressService : IShippingAddressService
    {
        private readonly IShippingAddressRepository _shippingAddressRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ShippingAddressService(IShippingAddressRepository shippingAddressRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _shippingAddressRepository = shippingAddressRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ShippingAddressResponseDto> AddShippingAddress(CreateShippingAddressDto addressDto)
        {
            var userId = GetUserId();
            if(addressDto.IsDefault == true)
            {
                await MakeEveryAddressNotDefault(userId);
            }
            var shippingAddress = _mapper.Map<ShippingAddress>(addressDto);
            shippingAddress.UserId = userId;

            var addAddress = await _shippingAddressRepository.AddShippingAddress(shippingAddress);
            var response = _mapper.Map<ShippingAddressResponseDto>(addAddress);
            return response;
        }

        public async Task<IEnumerable<ShippingAddressResponseDto>> GetAllShippingAddresses()
        {
            var userId = GetUserId();

            var userAddresses = await _shippingAddressRepository.GetAllShippingAddresses(userId);
            var response = _mapper.Map<IEnumerable<ShippingAddressResponseDto>>(userAddresses);
            return response;
        }

        public async Task<ShippingAddressResponseDto?> GetShippingAddressById(Guid id)
        {
            var userId = GetUserId();
            var userAddress = await GetUserAddress(id, userId);
            if (userAddress == null)
                return null;

            var response = _mapper.Map<ShippingAddressResponseDto>(userAddress);
            return response;
        }

        public  async Task<bool> EditShippingAddress(Guid id, UpdateShippingAddressDto addressDto)
        {
            var userId = GetUserId();
            var userAddress = await GetUserAddress(id, userId);
            if (userAddress == null)
                return false;

            if(addressDto.IsDefault == true)
            {
                await MakeEveryAddressNotDefault(userId);
            }

            _mapper.Map(addressDto, userAddress);
            await _shippingAddressRepository.Save();
            return true;
        }

        public async Task<bool> DeleteShippingAddress(Guid id)
        {
            var userId = GetUserId();
            var userAddress = await GetUserAddress(id, userId);
            if (userAddress == null)
                return false;

            await _shippingAddressRepository.DeleteShippingAddress(userAddress);
            return true;
        }




        private Guid GetUserId()
        {
            var userId = Guid.Parse(_httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
            return userId;
        }

        private async Task<ShippingAddress?> GetUserAddress(Guid id, Guid userId)
        {
            var userAddress = await _shippingAddressRepository.GetShippingAddressById(id, userId);
            return userAddress;
        }

        private async Task MakeEveryAddressNotDefault(Guid userId)
        {
            var userAddresses = await _shippingAddressRepository.GetAllShippingAddresses(userId);
            foreach (var item in userAddresses)
                if (item.IsDefault)
                    item.IsDefault = false;
        }
    }
}
