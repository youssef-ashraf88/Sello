using AutoMapper;
using Sello.Application.DTO;
using Sello.Domain.Entities;
using Sello.Domain.Entities.Identity;
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
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.AverageRating, opt => opt.
                 MapFrom(src => src.Reviews.Any()? src.Reviews.Average(r => (double)r.Rating) : 0))
                .ForMember(dest => dest.ReviewCount, opt => opt.MapFrom(src => src.Reviews.Count()));

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

            CreateMap<Order, ShippingAddressResponseDto>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.IsDefault, opt => opt.Ignore());

            //Order maps
            CreateMap<Order, CheckoutResponseDto>()
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.OrderItems))
                .ForMember(dest => dest.ShippingAddress, opt => opt.MapFrom(src => src));

            CreateMap<Order, OrderHistoryResponseDto>()
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Id));

            CreateMap<Order, OrderDetailsResponseDto>()
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ShippingAddress, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.OrderItems));

            //Order item maps
            CreateMap<OrderItem, OrderItemResponseDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductNameSnapshot))
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPriceSnapshot))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.UnitPriceSnapshot * src.Quantity));

            //Review maps
            CreateMap<CreateOrUpdateReviewDto, Review>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<Review, ReviewResponseDto>();

            //Users maps
            CreateMap<ApplicationUser, UserResponseDto>();
        }
    }
}
