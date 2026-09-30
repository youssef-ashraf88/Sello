using Sello.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sello.Domain.RepositoryContracts
{
    public interface IOrderItemRepository
    {
        Task AddOrderItem(OrderItem orderItem);
    }
}
