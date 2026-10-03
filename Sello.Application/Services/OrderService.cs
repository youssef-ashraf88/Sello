using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Sello.Application.DTO;
using Sello.Application.Exceptions;
using Sello.Application.ServiceContracts;
using Sello.Domain.Entities;
using Sello.Domain.Enums;
using Sello.Domain.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Sello.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICartRepository _cartRepository;
        private readonly IShippingAddressRepository _shippingAddressRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IMapper _mapper;
        
        public OrderService(IHttpContextAccessor httpContextAccessor, ICartRepository cartRepository, IShippingAddressRepository shippingAddressRepository, IOrderRepository orderRepository, IOrderItemRepository orderItemRepository, IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _cartRepository = cartRepository;
            _shippingAddressRepository = shippingAddressRepository;
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _mapper = mapper;
        }

        public async Task<CheckoutResponseDto> Checkout(CheckoutRequestDto request)
        {
            var userId = GetUserId();

            await _orderRepository.BeginTransaction();

            try
            {
                var cart = await _cartRepository.GetCartByUserId(userId);
                if (cart == null || !cart.CartItems.Any())
                    throw new InvalidOperationException("Cart is empty.");

                var shippingAddress = await _shippingAddressRepository.GetShippingAddressById(request.ShippingAddressId, userId);
                if (shippingAddress == null)
                    throw new InvalidOperationException("Shipping address not found.");

                foreach (var item in cart.CartItems)
                {
                    if (item.Product == null)
                        throw new InvalidOperationException("Product not found.");

                    if (item.Product.StockQuantity < item.Quantity)
                        throw new InvalidOperationException($"Not enough stock for product: {item.Product.Name}");
                }

                var order = new Order
                {
                    UserId = userId,
                    Status = OrderStatus.Pending,
                    CreatedAt = DateTime.UtcNow,

                    FullName = shippingAddress.FullName,
                    AddressLine = shippingAddress.AddressLine,
                    City = shippingAddress.City,
                    State = shippingAddress.State,
                    PostalCode = shippingAddress.PostalCode,
                    Country = shippingAddress.Country,
                    PhoneNumber = shippingAddress.PhoneNumber,

                    TotalAmount = 0
                };

                await _orderRepository.AddOrder(order);

                foreach (var item in cart.CartItems)
                {
                    var orderItem = new OrderItem
                    {
                        OrderId = order.Id,
                        ProductId = item.ProductId,
                        ProductNameSnapshot = item.Product!.Name,
                        UnitPriceSnapshot = item.Product.Price,
                        Quantity = item.Quantity
                    };
                    await _orderItemRepository.AddOrderItem(orderItem);
                    order.TotalAmount += item.Product.Price * item.Quantity;
                    item.Product.StockQuantity -= item.Quantity;
                }

                foreach (var item in cart.CartItems)
                {
                    await _cartRepository.DeleteCartItem(item);
                }
                await _orderRepository.Save();
                await _orderRepository.CommitTransaction();

                return _mapper.Map<CheckoutResponseDto>(order);
            }
            catch
            {
                await _orderRepository.RollbackTransaction();
                throw;
            }
        }

        public async Task<PagedResultResponseDto<OrderHistoryResponseDto>> GetAllOrders(int pageNumber, int pageSize)
        {
            var userId = GetUserId();
            CheckingPaginationNumbers(ref pageNumber, ref pageSize);

            var query = await _orderRepository.GetUserOrders(userId);
            var totalCount = await query.CountAsync();
            var orders = await query.OrderByDescending(o => o.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var orderDto = _mapper.Map<List<OrderHistoryResponseDto>>(orders);
            return new PagedResultResponseDto<OrderHistoryResponseDto>
            {
                Items = orderDto,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }

        public async Task<OrderDetailsResponseDto?> GetOrderById(Guid id)
        {
            var userId = GetUserId();
            var order = await _orderRepository.GetUserOrderById(id, userId);
            if (order == null)
                throw new NotFoundException("Order not found");

            return _mapper.Map<OrderDetailsResponseDto>(order);
        }

        public async Task<PagedResultResponseDto<OrderHistoryResponseDto>> GetAllOrdersForAdmin(OrderStatus? status, int pageNumber, int pageSize)
        {
            var query = await _orderRepository.GetAllOrders(status);
            CheckingPaginationNumbers(ref pageNumber, ref pageSize);
            var totalCount = await query.CountAsync();
            var orders = await query.OrderByDescending(o => o.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var response = _mapper.Map<List<OrderHistoryResponseDto>>(orders);
            return new PagedResultResponseDto<OrderHistoryResponseDto>
            {
                Items = response,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }

        public async Task<OrderDetailsResponseDto?> UpdateOrderStatus(Guid orderId, UpdateOrderStatusRequestDto newStatus)
        {
            var order = await _orderRepository.GetUserOrderById(orderId, null);
            if (order == null)
                throw new NotFoundException("Order not found.");

            if (!IsValidStatusTransition(order.Status, newStatus.Status))
                throw new InvalidOperationException(
                    $"Cannot change order status from {order.Status} to {newStatus}.");

            order.Status = newStatus.Status;

            await _orderRepository.Save();

            return _mapper.Map<OrderDetailsResponseDto>(order);
        }



        private Guid GetUserId()
        {
            return Guid.Parse(_httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
        }
        private void CheckingPaginationNumbers(ref int pageNumber, ref int pageSize)
        {
            const int maxPageSize = 50;
            if (pageSize > maxPageSize)
                pageSize = maxPageSize;

            if (pageNumber < 1)
                pageNumber = 1;

            if (pageSize < 1)
                pageSize = 10;
        }
        private bool IsValidStatusTransition(OrderStatus currentStatus, OrderStatus newStatus)
        {
            return currentStatus switch
            {
                OrderStatus.Pending =>
                    newStatus == OrderStatus.Processing ||
                    newStatus == OrderStatus.Cancelled,

                OrderStatus.Processing =>
                    newStatus == OrderStatus.Shipped ||
                    newStatus == OrderStatus.Cancelled,

                OrderStatus.Shipped =>
                    newStatus == OrderStatus.Delivered,

                OrderStatus.Delivered => false,

                OrderStatus.Cancelled => false,

                _ => false
            };
        }

    }
}
