using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories;

public class CartRepository : ICartRepository
{
    private readonly AppDbContext _context;

    public CartRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<CartItem>> GetAllAsync(string userId)
    {
        return await _context.CartItems
            .AsNoTracking()
            .Where(item => item.UserId == userId)
            .Include(item => item.Product)
            .OrderBy(item => item.Id)
            .ToListAsync();
    }

    public async Task<CartItem?> GetItemAsync(string userId, int productId)
    {
        return await _context.CartItems
            .FirstOrDefaultAsync(item =>
                item.UserId == userId && item.ProductId == productId);
    }

    public async Task AddAsync(CartItem cartItem)
    {
        await _context.CartItems.AddAsync(cartItem);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(CartItem cartItem)
    {
        cartItem.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(string userId, int productId)
    {
        var cartItem = await GetItemAsync(userId, productId);

        if (cartItem is null)
        {
            return false;
        }

        _context.CartItems.Remove(cartItem);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task ClearAsync(string userId)
    {
        var cartItems = await _context.CartItems
            .Where(item => item.UserId == userId)
            .ToListAsync();

        _context.CartItems.RemoveRange(cartItems);
        await _context.SaveChangesAsync();
    }
}