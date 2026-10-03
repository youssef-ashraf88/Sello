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

            return CreatedAtAction(nameof(GetProductById), new { id = newProduct.Id }, newProduct);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> EditProduct(Guid id, UpdateProductDto updateProductDto)
        {
            var updatedProduct = await _productService.EditProduct(id, updateProductDto);

            return Ok("Product updated successfully!");
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteProduct(Guid id)
        {
            var deletedProduct = await _productService.DeleteProduct(id);

            return Ok("Product deleted successfullty!");
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<PagedResultResponseDto<ProductResponseDto>>> GetAllProducts([FromQuery] ProductQueryParamsDto productQueryParamsDto)
        {
            var products = await _productService.GetAllProducts(productQueryParamsDto);

            return Ok(products);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ProductResponseDto>> GetProductById(Guid id)
        {
            var product = await _productService.GetProductById(id);

            return Ok(product);
        }

        
    }
}
