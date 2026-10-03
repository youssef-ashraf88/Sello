using Sello.Application.DTO;
using Sello.Application.ServiceContracts;
using Sello.Domain.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sello.Application.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;

        public DashboardService(IUserRepository userRepository, IProductRepository productRepository, IOrderRepository orderRepository)
        {
            _userRepository = userRepository;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
        }

        public async Task<DashboardResponseDto> GetDashboardStatistics()
        {
            var totalUsers = await _userRepository.GetTotalUsers();
            var totalProducts = await _productRepository.GetTotalProducts();
            var totalOrders = await _orderRepository.GetTotalOrders();
            var totalRevenue = await _orderRepository.GetTotalRevenue();

            return new DashboardResponseDto
            {
                TotalUsers = totalUsers,
                TotalProducts = totalProducts,
                TotalOrders = totalOrders,
                TotalRevenue = totalRevenue
            };
        }
    }
}
