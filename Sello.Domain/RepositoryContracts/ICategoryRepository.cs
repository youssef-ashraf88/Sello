using Sello.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sello.Domain.RepositoryContracts
{
    public interface ICategoryRepository
    {
        Task AddCategory(Category category);

        Task DeleteCategory(Category category);

        Task<Category?> GetCategoryById(Guid id);

        Task<Category?> GetCategoryByName(string name);

        Task<IEnumerable<Category>> GetAllCategories();

        Task Save();
    }
}
