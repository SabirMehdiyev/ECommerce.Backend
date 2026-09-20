using ECommerce.Application.DTOs.ProductDtos;
using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Application.Interfaces.Services;

namespace ECommerce.Application.Implementations.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public Task<ProductResponse> CreateAsync(CreateProductRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<ProductResponse>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ProductResponse?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ProductResponse?> UpdateAsync(int id, UpdateProductRequest request)
    {
        throw new NotImplementedException();
    }
}
