using Sello.Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sello.Application.ServiceContracts
{
    public interface IProductService
    {
        Task<ProductResponseDto?> CreateProduct(CreateProductDto createProductDto);

        Task<bool> DeleteProduct(Guid id);

        Task<bool> EditProduct(Guid id, UpdateProductDto newProduct);

        Task<IEnumerable<ProductResponseDto>> GetAllProducts();

        Task<ProductResponseDto?> GetProductById(Guid id);
    }
}
