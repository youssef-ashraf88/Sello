using Sello.Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sello.Application.ServiceContracts
{
    public interface ICartService
    {
        Task<CartResponseDto> GetOrCreateUserCart();

        Task<CartResponseDto?> AddItemToCart(CartItemAddRequestDto cartItemAddRequestDto);

        Task<CartResponseDto?> EditExistingCart(Guid id, CartItemUpdateRequestDto cartItemUpdateRequestDto);

        Task<CartResponseDto?> DeleteItemFromCart(Guid id);
    }
}
