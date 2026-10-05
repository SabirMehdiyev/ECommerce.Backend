using AutoMapper;
using ECommerce.Application.DTOs.ProductDtos;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Mappings;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<CreateProductRequest,Product>().ReverseMap();
        CreateMap<UpdateProductRequest,Product>().ReverseMap();
        CreateMap<ProductResponse,Product>().ReverseMap();
    }
}
