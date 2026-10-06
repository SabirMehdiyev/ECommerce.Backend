using ECommerce.Application.DTOs.CategoryDtos;
using FluentValidation;

namespace ECommerce.Application.Validators.CategoryDtos;

public class CreateCategoryRequestValidator : AbstractValidator<CreateCategoryRequest>
{
    public CreateCategoryRequestValidator()
    {
        RuleFor(request => request.Name).NotEmpty().MaximumLength(200);
        RuleFor(request => request.Description).MaximumLength(2000);
    }
}
