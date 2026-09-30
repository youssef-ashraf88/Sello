using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Sello.Domain.Entities;
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

        public async Task<Order?> GetUserOrderById(Guid orderId, Guid userId)
        {
            return await _db.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == orderId && o.UserId == userId);
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
