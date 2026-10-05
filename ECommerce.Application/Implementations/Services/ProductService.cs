using AutoMapper;
using ECommerce.Application.DTOs.ProductDtos;
using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Application.Interfaces.Services;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Implementations.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ProductResponse> CreateAsync(CreateProductRequest request)
    {
        var product = _mapper.Map<Product>(request);

        var createdProduct = await _repository.AddAsync(product);

        return _mapper.Map<ProductResponse>(createdProduct);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var product = await _repository.GetByIdAsync(id);

        if (product is null)
        {
            return false;
        }

        await _repository.DeleteAsync(product);

        return true;
    }

    public async Task<List<ProductResponse>> GetAllAsync()
    {
        var products = await _repository.GetAllAsync();

        return _mapper.Map<List<ProductResponse>>(products);    
    }

    public async Task<ProductResponse?> GetByIdAsync(int id)
    {
        var product = await _repository.GetByIdAsync(id);
        if (product == null)
        {
            return null;
        }

        return _mapper.Map<ProductResponse>(product);
    }

    public async Task<ProductResponse?> UpdateAsync(int id, UpdateProductRequest request)
    {
        var existingProduct = await _repository.GetByIdAsync(id);

        if (existingProduct is null) 
        {
            return null;
        }

        _mapper.Map(request, existingProduct);

        await _repository.UpdateAsync(existingProduct);

        return _mapper.Map<ProductResponse>(existingProduct);
    }
}
