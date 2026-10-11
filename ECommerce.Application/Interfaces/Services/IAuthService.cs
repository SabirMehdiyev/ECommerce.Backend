using ECommerce.Application.DTOs.AuthDtos;

namespace ECommerce.Application.Interfaces.Services;

public interface IAuthService
{
    Task RegisterAsync(RegisterRequest request);
    Task<LoginResponse?> LoginAsync(LoginRequest request);
}
