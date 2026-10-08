using ECommerce.Application.DTOs.AuthDtos;
using ECommerce.Application.Exceptions;
using ECommerce.Application.Interfaces.Services;
using ECommerce.Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace ECommerce.Application.Implementations.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IValidator<RegisterRequest> _registerValidator;
    private readonly IValidator<LoginRequest> _loginValidator;
    private readonly ITokenService _tokenService;

    public AuthService(UserManager<AppUser> userManager, IValidator<RegisterRequest> registerValidator, IValidator<LoginRequest> loginValidator, SignInManager<AppUser> signInManager, ITokenService tokenService)
    {
        _userManager = userManager;
        _registerValidator = registerValidator;
        _loginValidator = loginValidator;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest request)
    {
        var validationResult = await _loginValidator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null)
        {
            throw new BusinessRuleException("Email və ya parol yanlışdır.");
        }

        var result = await _signInManager.CheckPasswordSignInAsync(
            user,
            request.Password,
            lockoutOnFailure: true);

        if (!result.Succeeded)
        {
            throw new BusinessRuleException("Email və ya parol yanlışdır.");
        }

        return _tokenService.GenerateToken(user);
    }

    public async Task RegisterAsync(RegisterRequest request)
    {
        var validationResult = await _registerValidator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        AppUser user = new()
        {
            FullName = request.FullName,
            Email = request.Email,
            UserName = request.Email
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded) 
        {
            throw new BusinessRuleException(string.Join("; ", result.Errors.Select(error => error.Description)));
        }
    }
}
