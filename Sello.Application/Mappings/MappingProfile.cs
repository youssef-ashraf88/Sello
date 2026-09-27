using AutoMapper;
using Sello.Application.DTO;
using Sello.Domain.Entities;
using System;
using System.Collections.Generic;
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
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category!.Name));
        }
    }
}
