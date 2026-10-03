using AutoMapper;
using Microsoft.AspNetCore.Http;
using Sello.Application.DTO;
using Sello.Application.Exceptions;
using Sello.Application.ServiceContracts;
using Sello.Domain.Entities;
using Sello.Domain.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Sello.Application.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartService(ICartRepository cartRepository, IProductRepository productRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<CartResponseDto> GetOrCreateUserCart()
        {
            var userCart = await GetOrCreateCart();   

            var response = _mapper.Map<CartResponseDto>(userCart);
            CalculateCartTotals(response);

            return response;
        }

        public async Task<CartResponseDto?> AddItemToCart(CartItemAddRequestDto item)
        {
            var existingProduct = await _productRepository.GetProductById(item.ProductId);
            if (existingProduct == null)
                throw new NotFoundException("Product not found.");

            if (!existingProduct.IsActive)
                throw new BadRequestException("Product is not available.");

            if (item.Quantity <= 0)
                throw new BadRequestException("Quantity must be greater than zero.");

            if (item.Quantity > existingProduct.StockQuantity)
                throw new BadRequestException("Request quantity exceeds available stock.");               

            var userCart = await GetOrCreateCart();
            var existingItem = userCart.CartItems?.FirstOrDefault(x => x.ProductId == item.ProductId);

            if (existingItem == null)
                userCart.CartItems!.Add(new CartItem { ProductId = item.ProductId, Quantity = item.Quantity });
            else
            {
                if (existingItem.Quantity + item.Quantity > existingProduct.StockQuantity)
                    throw new BadRequestException("Requested quantity exceeds available stock.");

                existingItem.Quantity += item.Quantity;
            }

            await _cartRepository.Save();

            var response = _mapper.Map<CartResponseDto>(userCart);
            CalculateCartTotals(response);

            return response;
        }

        public async Task<CartResponseDto?> EditExistingCart(Guid id, CartItemUpdateRequestDto request)
        {
            var userCart = await GetOrCreateCart();
            var item = userCart.CartItems?.FirstOrDefault(p => p.Id == id);
            if (item == null)
                throw new NotFoundException("Cart item not found.");

            var product = await _productRepository.GetProductById(id);
            if (request.Quantity > product.StockQuantity)
                throw new BadRequestException("Requested quantity exceeds available stock.");

            item.Quantity = request.Quantity;

            await _cartRepository.Save();

            var resposne = _mapper.Map<CartResponseDto>(userCart);
            CalculateCartTotals(resposne);

            return resposne;
        }

        public async Task<CartResponseDto?> DeleteItemFromCart(Guid id)
        {
            var userCart = await GetOrCreateCart();
            var item = userCart.CartItems?.FirstOrDefault(p => p.Id == id);
            if (item == null)
                throw new NotFoundException("Cart item not found.");

            await _cartRepository.DeleteCartItem(item);
            await _cartRepository.Save();
            var resposne = _mapper.Map<CartResponseDto>(userCart);
            CalculateCartTotals(resposne);

            return resposne;
        }







        //GetOrCreateCart Private Method
        private async Task<Cart> GetOrCreateCart()
        {
            var userId = Guid.Parse(_httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

            //if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userId))
            //{
            //    throw new UnauthorizedAccessException();
            //}

            var userCart = await _cartRepository.GetCartByUserId(userId);
            if (userCart == null)
            {
                userCart = await _cartRepository.CreateCart(new Cart
                {
                    UserId = userId
                });
            }

            return userCart;
        }

        private void CalculateCartTotals(CartResponseDto response)
        {
            foreach (var cartItem in response.CartItems)
            {
                cartItem.Total = cartItem.Quantity * cartItem.Price;
            }

            response.Total = response.CartItems.Sum(item => item.Total);
        }
    }
}
