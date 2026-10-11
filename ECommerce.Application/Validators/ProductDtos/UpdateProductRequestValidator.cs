using ECommerce.Application.DTOs.ProductDtos;
using FluentValidation;

namespace ECommerce.Application.Validators.ProductDtos;

public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
{
    public UpdateProductRequestValidator()
    {
        RuleFor(request => request.Name).NotEmpty().MaximumLength(200);
        RuleFor(request => request.Description).MaximumLength(2000);
        RuleFor(request => request.Price).GreaterThan(0).PrecisionScale(18, 2, true);
        RuleFor(request => request.Stock).GreaterThanOrEqualTo(0);
        RuleFor(request => request.CategoryId).GreaterThan(0);
        RuleFor(x => x.ImageUrl)
           .MaximumLength(2048)
           .Must(url => Uri.TryCreate(url, UriKind.Absolute, out var uri) && uri.Scheme == Uri.UriSchemeHttps)
           .When(x => x.ImageUrl != null)
           .WithMessage("Şəklin URL-i düzgün HTTPS linki olmalıdır.");
    }
}
