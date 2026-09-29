using Sello.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sello.Domain.RepositoryContracts
{
    public interface IShippingAddressRepository
    {
        Task<ShippingAddress> AddShippingAddress(ShippingAddress address);
        Task DeleteShippingAddress(ShippingAddress address);
        Task<ShippingAddress?> GetShippingAddressById(Guid id, Guid userId);
        Task<IEnumerable<ShippingAddress>> GetAllShippingAddresses(Guid userId);
        Task Save();
    }
}
