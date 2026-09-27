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

        public IQueryable<Product> GetAllProducts(ProductQueryParams productQueryParams)
        {
            var query = _db.Products.Where(p => p.IsActive).AsNoTracking();

            query = query.Applyfilters(productQueryParams); // search handel
            query = query.ApplySorting(productQueryParams); // sort handel

            return query;
        }

        public IQueryable<Product> GetAllProductsForAdmins(ProductQueryParams productQueryParams)
        {
            var query = _db.Products.AsNoTracking().AsQueryable();

            query = query.Applyfilters(productQueryParams);
            query = query.ApplySorting(productQueryParams);

            return query;
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

    public static class QueryFilter
    {
        public static IQueryable<Product> Applyfilters(this IQueryable<Product> query, ProductQueryParams parameter)
        {
            if (!string.IsNullOrWhiteSpace(parameter.Search))
                query = query.Where(p => p.Name!.Contains(parameter.Search));

            if (parameter.CategoryId.HasValue)
                query = query.Where(p => p.CategoryId == parameter.CategoryId);

            if (parameter.MinPrice.HasValue)
                query = query.Where(p => p.Price >= parameter.MinPrice);

            if (parameter.MaxPrice.HasValue)
                query = query.Where(p => p.Price <= parameter.MaxPrice);

            return query;
        }

        public static IQueryable<Product> ApplySorting(this IQueryable<Product> query, ProductQueryParams parameter)
        {
            if (string.IsNullOrWhiteSpace(parameter.SortBy))
                return query;

            bool descending = parameter.SortOrder?.ToLower() == "desc";

            return parameter.SortBy.ToLower() switch
            {
                "price" => descending ? query.OrderByDescending(p => p.Price) : query.OrderBy(p => p.Price),

                "name" => descending ? query.OrderByDescending(p => p.Name) : query.OrderBy(p => p.Name),

                _ => query
            };
        }
    }
}
