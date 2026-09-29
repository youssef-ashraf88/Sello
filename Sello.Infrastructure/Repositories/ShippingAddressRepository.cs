using Microsoft.EntityFrameworkCore;
using Sello.Domain.Entities;
using Sello.Domain.RepositoryContracts;
using Sello.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sello.Infrastructure.Repositories
{
    public class ShippingAddressRepository : IShippingAddressRepository
    {
        private readonly ApplicationDbContext _db;

        public ShippingAddressRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ShippingAddress> AddShippingAddress(ShippingAddress address)
        {
            await _db.ShippingAddresses.AddAsync(address);
            await Save();

            return address;
        }

        public async Task DeleteShippingAddress(ShippingAddress address)
        {
            _db.ShippingAddresses.Remove(address);
            await Save();
        }

        public async Task<IEnumerable<ShippingAddress>> GetAllShippingAddresses(Guid userId)
        {
            var shippingAddresses = await _db.ShippingAddresses.Where(sa => sa.UserId == userId).ToListAsync();
            return shippingAddresses;
        }

        public async Task<ShippingAddress?> GetShippingAddressById(Guid id, Guid userId)
        {
            var shippingAddress = await _db.ShippingAddresses.FirstOrDefaultAsync(sa => sa.Id == id && sa.UserId == userId);
            return shippingAddress;
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }
    }
}
