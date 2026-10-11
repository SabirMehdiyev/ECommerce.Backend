using ECommerce.Application.DTOs.CartDtos;
using FluentValidation;

namespace ECommerce.Application.Validators.CartDtos;

public class AddCartItemRequestValidator : AbstractValidator<AddCartItemRequest>
{
    public AddCartItemRequestValidator()
    {
        RuleFor(request => request.ProductId).GreaterThan(0);
        RuleFor(request => request.Quantity).GreaterThan(0);
    }
}
