namespace ECommerce.Application.DTOs.AuthDtos;

public class LoginResponse
{
    public required string AccessToken { get; set; }
    public DateTime ExpiresAt { get; set; }
}
