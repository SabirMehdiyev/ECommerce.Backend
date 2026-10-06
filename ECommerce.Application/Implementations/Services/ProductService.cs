using AutoMapper;
using ECommerce.Application.Exceptions;
using FluentValidation;
using ECommerce.Application.DTOs.ProductDtos;
using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Application.Interfaces.Services;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Implementations.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IValidator<CreateProductRequest> _createValidator;
    private readonly IValidator<UpdateProductRequest> _updateValidator;

    public ProductService(IProductRepository repository, IMapper mapper,
        ICategoryRepository categoryRepository, IValidator<CreateProductRequest> createValidator,
        IValidator<UpdateProductRequest> updateValidator)
    {
        _repository = repository;
        _mapper = mapper;
        _categoryRepository = categoryRepository;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    public async Task<ProductResponse> CreateAsync(CreateProductRequest request)
    {
        var validationResult = await _createValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var category = await _categoryRepository.GetByIdAsync(request.CategoryId);
        if (category is null)
        {
            throw new BusinessRuleException("The selected category does not exist.");
        }

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

        var validationResult = await _updateValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var category = await _categoryRepository.GetByIdAsync(request.CategoryId);
        if (category is null)
        {
            throw new BusinessRuleException("The selected category does not exist.");
        }

        _mapper.Map(request, existingProduct);

        await _repository.UpdateAsync(existingProduct);

        return _mapper.Map<ProductResponse>(existingProduct);
    }
}
