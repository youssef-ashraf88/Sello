using Sello.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sello.Domain.RepositoryContracts
{
    public interface IOrderRepository
    {
        Task AddOrder(Order order);
        Task<IQueryable<Order>> GetUserOrders(Guid userId);
        Task<Order?> GetUserOrderById(Guid orderId, Guid userId);
        Task Save();
        Task BeginTransaction();
        Task CommitTransaction();
        Task RollbackTransaction();
    }
}
