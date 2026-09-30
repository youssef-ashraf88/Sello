using Microsoft.EntityFrameworkCore;
using Sello.Domain.Entities;
using Sello.Domain.RepositoryContracts;
using Sello.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sello.Infrastructure.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _db;

        public CartRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Cart> CreateCart(Cart cart)
        {
            await _db.Carts.AddAsync(cart);
            await Save();

            return cart;
        }

        public async Task<Cart?> GetCartByUserId(Guid id)
        {
            var cart = await _db.Carts
                .Include(c => c.CartItems!)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserId == id);

            return cart;
        }

        public async Task DeleteCartItem(CartItem cartItem)
        {
            _db.Remove(cartItem);
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }
    }
}
