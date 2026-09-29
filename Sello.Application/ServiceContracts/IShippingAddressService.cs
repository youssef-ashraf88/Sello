using Sello.Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sello.Application.ServiceContracts
{
    public interface IShippingAddressService
    {
        Task<ShippingAddressResponseDto> AddShippingAddress(CreateShippingAddressDto addressDto);

        Task<IEnumerable<ShippingAddressResponseDto>> GetAllShippingAddresses();

        Task<ShippingAddressResponseDto?> GetShippingAddressById(Guid id);

        Task<bool> EditShippingAddress(Guid id, UpdateShippingAddressDto addressDto);

        Task<bool> DeleteShippingAddress(Guid id);
    }
}
