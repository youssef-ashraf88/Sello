using AutoMapper;
using Sello.Application.DTO;
using Sello.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Sello.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Category maps
            CreateMap<CreateCategoryDto, Category>();

            CreateMap<UpdateCategoryDto, Category>();

            CreateMap<Category, CategoryResponseDto>();

            //Product maps
            CreateMap<CreateProductDto, Product>();

            CreateMap<UpdateProductDto, Product>();

            CreateMap<Product, ProductResponseDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));

            CreateMap<ProductQueryParamsDto, ProductQueryParams>();

            //Cart maps
            CreateMap<Cart, CartResponseDto>()
                .ForMember(dest => dest.CartId, opt => opt.MapFrom(src => src.Id));

            CreateMap<CartItem, CartItemResponse>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product!.Name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product!.Price))
                .ForMember(dest => dest.CartItemId, opt => opt.MapFrom(src => src.Id));

            //Shipping Address maps
            CreateMap<CreateShippingAddressDto, ShippingAddress>();

            CreateMap<UpdateShippingAddressDto, ShippingAddress>();

            CreateMap<ShippingAddress, ShippingAddressResponseDto>();
        }
    }
}
