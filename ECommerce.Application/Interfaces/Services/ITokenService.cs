using ECommerce.Application.DTOs.AuthDtos;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Interfaces.Services;

public interface ITokenService
{
    LoginResponse GenerateToken(AppUser user, IEnumerable<string> roles);
}
