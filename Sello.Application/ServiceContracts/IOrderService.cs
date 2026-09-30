using Sello.Application.DTO;
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
    }
}
