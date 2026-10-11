using AutoMapper;
using ECommerce.Application.DTOs.CartDtos;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Mappings;

public class CartProfile : Profile
{
    public CartProfile()
    {
        CreateMap<CartItem, CartItemResponse>()
            .ForMember(destination => destination.ProductName,
                options => options.MapFrom(source => source.Product.Name))
            .ForMember(destination => destination.UnitPrice,
                options => options.MapFrom(source => source.Product.Price));
    }
}
