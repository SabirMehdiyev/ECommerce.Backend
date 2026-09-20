using ECommerce.Domain.Common;

namespace ECommerce.Application.Interfaces.Services;

public interface IRepository<T> where T : BaseEntity
{
    Task<List<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<T> DeleteAsync(T entity);



}
