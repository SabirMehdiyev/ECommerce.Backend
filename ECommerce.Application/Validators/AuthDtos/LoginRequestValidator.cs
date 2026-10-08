using ECommerce.Application.DTOs.AuthDtos;
using FluentValidation;

namespace ECommerce.Application.Validators.AuthDtos;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x=>x.Email).NotEmpty().EmailAddress();
        RuleFor(x=>x.Password).NotEmpty();  
    }
}
