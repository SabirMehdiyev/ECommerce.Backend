using AutoMapper;
using ECommerce.Application.DTOs.CategoryDtos;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Mappings;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<CreateCategoryRequest,Category>().ReverseMap();
        CreateMap<UpdateCategoryRequest,Category>().ReverseMap();
        CreateMap<CategoryResponse,Category>().ReverseMap();
    }
}
