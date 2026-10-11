using ECommerce.Application.DTOs.CartDtos;

namespace ECommerce.Application.Interfaces.Services;

public interface ICartService
{
    Task<CartResponse> GetAsync(string userId);
    Task<CartResponse> AddAsync(string userId, AddCartItemRequest request);
    Task<CartResponse?> UpdateAsync(string userId, int productId, UpdateCartItemRequest request);
    Task<bool> DeleteAsync(string userId, int productId);
    Task ClearAsync(string userId);
}
