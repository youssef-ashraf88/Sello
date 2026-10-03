using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Sello.Application.DTO;
using Sello.Application.Exceptions;
using Sello.Application.ServiceContracts;
using Sello.Domain.Entities;
using Sello.Domain.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sello.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ProductResponseDto?> CreateProduct(CreateProductDto createProductDto)
        {
            var existingProduct = await _productRepository.GetProductByName(createProductDto.Name);
            if (existingProduct is not null)
                throw new BadRequestException("A product with this name already exists.");

            var existingCategory = await _categoryRepository.GetCategoryById(createProductDto.CategoryId);
            if (existingCategory == null)
                throw new NotFoundException("Category not found.");

            var newProduct = _mapper.Map<Product>(createProductDto);
            newProduct.Category = existingCategory;

            await _productRepository.AddProduct(newProduct);

            var productResponse = _mapper.Map<ProductResponseDto>(newProduct);
            return productResponse;
        }

        public async Task<bool> DeleteProduct(Guid id)
        {
            var product = await _productRepository.GetProductById(id);
            if (product == null)
                throw new NotFoundException("Product not found.");

            await _productRepository.DeleteProduct(product);
            return true;
        }

        public async Task<bool> EditProduct(Guid id, UpdateProductDto newProduct)
        {
            var product = await _productRepository.GetProductById(id);
            if (product == null)
                throw new NotFoundException("Product not found.");

            var existCategory = await _categoryRepository.GetCategoryById(newProduct.CategoryId);
            if (existCategory == null)
                throw new NotFoundException("Category not found.");
            
            _mapper.Map(newProduct, product);
            await _productRepository.Save();
            
            return true;
        }

        public async Task<PagedResultResponseDto<ProductResponseDto>> GetAllProducts(ProductQueryParamsDto productQueryParamsDto)
        {
            const int maxPageSize = 50;
            var pageNumber = productQueryParamsDto.PageNumber < 1 ? 1 : productQueryParamsDto.PageNumber;
            var pageSize = productQueryParamsDto.PageSize < 1 ? 10 : productQueryParamsDto.PageSize;
            if (pageSize > maxPageSize)
                pageSize = maxPageSize;
            

            var isAdmin = _httpContextAccessor.HttpContext?.User.IsInRole("Admin") ?? false;

            var productQueryParam = _mapper.Map<ProductQueryParams>(productQueryParamsDto);

            var productsQuery = isAdmin ? _productRepository.GetAllProductsForAdmins(productQueryParam) : _productRepository.GetAllProducts(productQueryParam);

            var totalCount = await productsQuery.CountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            var skip = (pageNumber - 1) * pageSize;

            var products = await productsQuery
                .Skip(skip)
                .Take(pageSize)
                .ProjectTo<ProductResponseDto>(_mapper.ConfigurationProvider)
                .ToListAsync();


            return new PagedResultResponseDto<ProductResponseDto>
            {
                Items = products,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages
            };
        }

        public async Task<ProductResponseDto?> GetProductById(Guid id)
        {
            var isAdmin = _httpContextAccessor.HttpContext?.User.IsInRole("Admin") ?? false;

            var productQuery = isAdmin ? _productRepository.GetProductQueryForAdminsById(id) : _productRepository.GetProductQueryById(id);

            var product = await productQuery
                .ProjectTo<ProductResponseDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            if (product == null)
                throw new NotFoundException("Product not found.");

            return product;
        }
    }
}
