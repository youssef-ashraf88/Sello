using Sello.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sello.Domain.RepositoryContracts
{
    public interface ICartRepository
    {
        Task<Cart> CreateCart(Cart cart);
        Task<Cart?> GetCartByUserId(Guid id);
        Task DeleteCartItem(CartItem cartItem);
        Task Save();
    }
}
