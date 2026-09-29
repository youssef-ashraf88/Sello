using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sello.Application.DTO;
using Sello.Application.ServiceContracts;
using Sello.Domain.Enums;
using Sello.Infrastructure.Repositories;

namespace Sello.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet]
        [Authorize(Roles = "Customer")]
        public async Task<ActionResult<CartResponseDto>> GetCart()
        {
            var cart = await _cartService.GetOrCreateUserCart();

            return Ok(cart);
        }

        [HttpPost("items")]
        public async Task<ActionResult<CartResponseDto>> AddItemToCart(CartItemAddRequestDto cartItemAddRequestDto)
        {
            var cart = await _cartService.AddItemToCart(cartItemAddRequestDto);
            if (cart == null)
                return NotFound("Invalid cart item request");

            return Ok(cart);
        }

        [HttpPut("items/{id}")]
        public async Task<ActionResult<CartResponseDto>> EditCartItemQuantity(Guid id, CartItemUpdateRequestDto cartItemUpdateRequestDto)
        {
            var cart = await _cartService.EditExistingCart(id, cartItemUpdateRequestDto);
            if (cart == null)
                return NotFound("Invalid cart item request");

            return Ok(cart);
        }

        [HttpDelete("items/{id}")]
        public async Task<ActionResult<CartResponseDto>> RemoveItemFromCart(Guid id)
        {
            var cart = await _cartService.DeleteItemFromCart(id);
            if (cart == null)
                return NotFound("Cart item not found");

            return Ok(cart);
        }
    }
}
