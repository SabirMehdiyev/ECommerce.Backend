using ECommerce.Application.DTOs.ProductDtos;
using FluentValidation;

namespace ECommerce.Application.Validators.ProductDtos;

public class ProductQueryRequestValidator : AbstractValidator<ProductQueryRequest>
{
    public ProductQueryRequestValidator()
    {
        RuleFor(x => x.Search)
            .MaximumLength(200);

        RuleFor(x => x.CategoryId)
            .GreaterThan(0)
            .When(x => x.CategoryId.HasValue);

        RuleFor(x => x.MinPrice)
            .GreaterThanOrEqualTo(0)
            .When(x => x.MinPrice.HasValue);

        RuleFor(x => x.MaxPrice)
            .GreaterThanOrEqualTo(0)
            .When(x => x.MaxPrice.HasValue);

        RuleFor(x => x.MaxPrice)
            .Must((request, maxPrice) =>
                maxPrice!.Value >= request.MinPrice!.Value)
            .When(x => x.MinPrice.HasValue && x.MaxPrice.HasValue)
            .WithMessage("MaxPrice must be greater than or equal to MinPrice.");

        RuleFor(x => x.SortBy)
            .Must(sortBy =>
                sortBy is null or "priceAsc" or "priceDesc" or "newest")
            .WithMessage("SortBy must be priceAsc, priceDesc, or newest.");

        RuleFor(x => x.PageNumber)
            .GreaterThan(0);

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 100);
    }
}