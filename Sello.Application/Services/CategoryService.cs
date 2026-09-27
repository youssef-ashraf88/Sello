using AutoMapper;
using Sello.Application.DTO;
using Sello.Application.ServiceContracts;
using Sello.Domain.Entities;
using Sello.Domain.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sello.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CategoryResponseDto> CreateCategory(CreateCategoryDto? category)
        {
            var existingCategory = await _categoryRepository.GetCategoryByName(category!.Name!);
            if (existingCategory is not null)
                throw new Exception("A category with this name already exists.");

            var newCategory = _mapper.Map<Category>(category);

            await _categoryRepository.AddCategory(newCategory);

            var categoryresponse = _mapper.Map<CategoryResponseDto>(newCategory);

            return categoryresponse;
        }

        public async Task<bool> DeleteCategory(Guid id)
        {
            var category = await _categoryRepository.GetCategoryById(id);
            if (category == null)
                return false;

            await _categoryRepository.DeleteCategory(category);
            return true;
        }

        public async Task<bool> EditCategory(Guid id, UpdateCategoryDto newCategory)
        {
            var category = await _categoryRepository.GetCategoryById(id);
            if (category == null)
                return false;

            _mapper.Map(newCategory, category);
            await _categoryRepository.Save();
            
            return true;
        }

        public async Task<IEnumerable<CategoryResponseDto>> GetAllCategories()
        {
            var categories = await _categoryRepository.GetAllCategories();
            var categoriesResponse = _mapper.Map<IEnumerable<CategoryResponseDto>>(categories);

            return categoriesResponse;
        }

        public async Task<CategoryResponseDto?> GetCategoryById(Guid id)
        {
            var category = await _categoryRepository.GetCategoryById(id);
            if (category == null)
                return null;

            var categoryResponse = _mapper.Map<CategoryResponseDto>(category);
            return categoryResponse;
        }
    }
}
