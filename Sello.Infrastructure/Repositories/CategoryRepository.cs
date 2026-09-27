using Microsoft.EntityFrameworkCore;
using Sello.Domain.Entities;
using Sello.Domain.RepositoryContracts;
using Sello.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sello.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task AddCategory(Category category)
        {
            await _db.AddAsync(category);
            await Save();
        }

        public async Task DeleteCategory(Category category)
        {
            _db.Remove(category);
            await Save();
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            var categories = await _db.Categories.AsNoTracking().ToListAsync();
            return categories;
        }

        public async Task<Category?> GetCategoryById(Guid id)
        {
            var category = await _db.Categories.FindAsync(id);
            return category;
        }

        public async Task<Category?> GetCategoryByName(string name)
        {
            return await _db.Categories.AsNoTracking().FirstOrDefaultAsync(c => c.Name == name);
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }
    }
}
