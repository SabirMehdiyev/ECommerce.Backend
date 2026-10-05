using ECommerce.Application.DTOs.ProductDtos;
using FluentValidation;

namespace ECommerce.Application.Validators.ProductDtos;

public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
{
    public CreateProductRequestValidator()
    {
        RuleFor(request => request.Name).NotEmpty().MaximumLength(200);
        RuleFor(request => request.Description).MaximumLength(2000);
        RuleFor(request => request.Price).GreaterThan(0).PrecisionScale(18, 2, true);
        RuleFor(request => request.Stock).GreaterThanOrEqualTo(0);
        RuleFor(request => request.CategoryId).GreaterThan(0);
    }
}
