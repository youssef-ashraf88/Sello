using Microsoft.EntityFrameworkCore;
using Sello.Domain.Entities;
using Sello.Domain.RepositoryContracts;
using Sello.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sello.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductRepository (ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task AddProduct(Product product)
        {
            await _db.Products.AddAsync(product);
            await Save();
        }

        public async Task DeleteProduct(Product product)
        {
            _db.Products.Remove(product);
            await Save();
        }

        public IQueryable<Product> GetAllProducts()
        {
            return _db.Products.AsNoTracking().AsQueryable().Where(p => p.IsActive);
        }

        public IQueryable<Product> GetAllProductsForAdmins()
        {
            return _db.Products.AsNoTracking().AsQueryable();
        }

        public async Task<Product?> GetProductById(Guid id)
        {
            var product = await _db.Products.FindAsync(id);
            return product;
        }

        public async Task<Product?> GetProductByName(string? name)
        {
            return await _db.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Name == name);
        }

        public IQueryable<Product> GetProductQueryById(Guid id)
        {
            return _db.Products.Where(p => p.Id == id && p.IsActive).AsNoTracking().AsQueryable();
        }

        public IQueryable<Product> GetProductQueryForAdminsById(Guid id)
        {
            return _db.Products.Where(p => p.Id == id).AsNoTracking().AsQueryable();
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }
    }
}
