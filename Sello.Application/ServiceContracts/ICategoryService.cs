using Sello.Application.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Sello.Application.ServiceContracts
{
    public interface ICategoryService
    {
        Task<CategoryResponseDto> CreateCategory(CreateCategoryDto category);

        Task<CategoryResponseDto?> GetCategoryById(Guid id);

        Task<IEnumerable<CategoryResponseDto>> GetAllCategories();

        Task<bool> DeleteCategory(Guid id);

        Task<bool> EditCategory(Guid id, UpdateCategoryDto newCategory);
    }
}
