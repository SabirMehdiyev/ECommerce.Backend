using ECommerce.Domain.Entities;

namespace ECommerce.Application.Interfaces.Repositories;

public interface ICartRepository
{
    Task<List<CartItem>> GetAllAsync(string userId);
    Task<CartItem?> GetItemAsync(string userId, int productId);
    Task AddAsync(CartItem cartItem);
    Task UpdateAsync(CartItem cartItem);
    Task<bool> DeleteAsync(string userId, int productId);
    Task ClearAsync(string userId);
}
