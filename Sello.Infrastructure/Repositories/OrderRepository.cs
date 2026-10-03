using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Sello.Domain.Entities;
using Sello.Domain.Enums;
using Sello.Domain.RepositoryContracts;
using Sello.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sello.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _db;
        private IDbContextTransaction? _transaction;

        public OrderRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task AddOrder(Order order)
        {
            await _db.Orders.AddAsync(order);
        }

        public async Task<IQueryable<Order>> GetUserOrders(Guid userId)
        {
            return _db.Orders.Where(o => o.UserId == userId);
        }

        public async Task<Order?> GetUserOrderById(Guid orderId, Guid? userId)
        {
            var order = _db.Orders
                .Include(o => o.OrderItems).AsQueryable();
            if(userId is not null)
                order = order.Where(o => o.UserId == userId);

            return await order.FirstOrDefaultAsync(o => o.Id == orderId);
        }

        public async Task<IQueryable<Order>> GetAllOrders(OrderStatus? status)
        {
            var query = _db.Orders.Include(o => o.OrderItems).AsQueryable();
            if (status.HasValue)
                query = query.Where(o => o.Status == status.Value);

            return query;
        }

        public async Task<int> GetTotalOrders()
        {
            return await _db.Orders.CountAsync();
        }

        public async Task<decimal> GetTotalRevenue()
        {
            return await _db.Orders
                .Where(o => o.Status != OrderStatus.Cancelled)
                .SumAsync(o => o.TotalAmount);
        }

        public async Task BeginTransaction()
        {
            _transaction = await _db.Database.BeginTransactionAsync();
        }

        public async Task CommitTransaction()
        {
            if (_transaction == null)
                throw new InvalidOperationException("Transaction has not been started.");

            await _transaction.CommitAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }

        public async Task RollbackTransaction()
        {
            if (_transaction == null)
                return;

            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }

        
    }
}
