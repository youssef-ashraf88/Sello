using Sello.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sello.Domain.RepositoryContracts
{
    public interface IProductRepository
    {
        Task AddProduct(Product product);

        Task DeleteProduct(Product product);

        IQueryable<Product> GetAllProducts();

        IQueryable<Product> GetAllProductsForAdmins();

        Task<Product?> GetProductById(Guid id);

        Task<Product?> GetProductByName(string? name);

        IQueryable<Product> GetProductQueryById(Guid id);

        IQueryable<Product> GetProductQueryForAdminsById(Guid id);

        Task Save();
    }
}
