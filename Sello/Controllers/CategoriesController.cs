using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sello.Application.DTO;
using Sello.Application.ServiceContracts;

namespace Sello.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<CategoryResponseDto>> CreateCategory(CreateCategoryDto createCategoryDto)
        {
            var newCategory = await _categoryService.CreateCategory(createCategoryDto);

            return CreatedAtAction(nameof(GetCategoryById), new { id = newCategory.Id }, newCategory);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> EditCategory(Guid id, UpdateCategoryDto updateCategoryDto)
        {
            var category = await _categoryService.EditCategory(id, updateCategoryDto);

            return Ok("Category updated successfully!");
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteCategory(Guid id)
        {
            var category = await _categoryService.DeleteCategory(id);

            return Ok("Category deleted successfully!");
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<CategoryResponseDto>>> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategories();

            return Ok(categories);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<CategoryResponseDto>> GetCategoryById(Guid id)
        {
            var category = await _categoryService.GetCategoryById(id);

            return Ok(category);
        }
    }
}
