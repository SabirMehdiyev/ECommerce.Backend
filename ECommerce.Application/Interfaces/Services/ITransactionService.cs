namespace ECommerce.Application.Interfaces.Services;

public interface ITransactionService
{
    Task ExecuteAsync(Func<Task> action);
}
