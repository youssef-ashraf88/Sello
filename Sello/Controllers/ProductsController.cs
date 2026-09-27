using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sello.Application.DTO;
using Sello.Application.ServiceContracts;

namespace Sello.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ProductResponseDto>> CreateProduct(CreateProductDto createProductDto)
        {
            var newProduct = await _productService.CreateProduct(createProductDto);
            if (newProduct == null)
                return NotFound("The category does not exist.");

            return CreatedAtAction(nameof(GetProductById), new { id = newProduct.Id }, newProduct);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> EditProduct(Guid id, UpdateProductDto updateProductDto)
        {
            var updatedProduct = await _productService.EditProduct(id, updateProductDto);
            if (!updatedProduct)
                return NotFound("Product or category does not exist");

            return Ok("Product updated successfully!");
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteProduct(Guid id)
        {
            var deletedProduct = await _productService.DeleteProduct(id);
            if (!deletedProduct)
                return NotFound("Product does not exist.");

            return Ok("Product deleted successfullty!");
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ProductResponseDto>>> GetAllProducts()
        {
            var products = await _productService.GetAllProducts();

            return Ok(products);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ProductResponseDto>> GetProductById(Guid id)
        {
            var product = await _productService.GetProductById(id);
            if (product == null)
                return NotFound("Product not found.");

            return Ok(product);
        }
    }
}
