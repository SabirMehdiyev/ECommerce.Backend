using ECommerce.Application.DTOs.CartDtos;
using FluentValidation;

namespace ECommerce.Application.Validators.CartDtos;

public class UpdateCartItemRequestValidator : AbstractValidator<UpdateCartItemRequest>
{
    public UpdateCartItemRequestValidator()
    {
        RuleFor(request => request.Quantity).GreaterThan(0);
    }
}
