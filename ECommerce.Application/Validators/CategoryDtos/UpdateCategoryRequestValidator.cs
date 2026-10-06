using ECommerce.Application.DTOs.CategoryDtos;
using FluentValidation;

namespace ECommerce.Application.Validators.CategoryDtos;

public class UpdateCategoryRequestValidator : AbstractValidator<UpdateCategoryRequest>
{
    public UpdateCategoryRequestValidator()
    {
        RuleFor(request => request.Name).NotEmpty().MaximumLength(200);
        RuleFor(request => request.Description).MaximumLength(2000);
    }
}
