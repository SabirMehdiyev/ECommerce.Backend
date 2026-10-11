using ECommerce.Application.Interfaces.Services;
using ECommerce.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Services;

public class TransactionService : ITransactionService
{
    private readonly AppDbContext _context;

    public TransactionService(AppDbContext context)
    {
        _context = context;
    }

    public async Task ExecuteAsync(Func<Task> action)
    {
        await using var transaction =
            await _context.Database.BeginTransactionAsync();

        await action();

        await transaction.CommitAsync();
    }
}