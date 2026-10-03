using Sello.Application.DTO;
using Sello.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sello.Application.ServiceContracts
{
    public interface IOrderService
    {
        Task<CheckoutResponseDto> Checkout(CheckoutRequestDto request);
        Task<PagedResultResponseDto<OrderHistoryResponseDto>> GetAllOrders(int pageNumber, int pageSize);
        Task<OrderDetailsResponseDto?> GetOrderById(Guid id);
        Task<PagedResultResponseDto<OrderHistoryResponseDto>> GetAllOrdersForAdmin(OrderStatus? status, int pageNumber, int pageSize);
        Task<OrderDetailsResponseDto?> UpdateOrderStatus(Guid orderId, UpdateOrderStatusRequestDto request);
    }
}
