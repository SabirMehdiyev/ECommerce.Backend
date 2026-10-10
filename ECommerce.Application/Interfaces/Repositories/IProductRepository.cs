using ECommerce.Domain.Entities;
using ECommerce.Application.DTOs;
using ECommerce.Application.DTOs.ProductDtos;
namespace ECommerce.Application.Interfaces.Repositories;

public interface IProductRepository : IRepository<Product>
{
    Task<PagedResponse<Product>> GetPagedAsync(ProductQueryRequest request);
}
