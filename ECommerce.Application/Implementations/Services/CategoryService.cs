using AutoMapper;
using ECommerce.Application.Exceptions;
using FluentValidation;
using ECommerce.Application.DTOs.CategoryDtos;
using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Application.Interfaces.Services;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Implementations.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repository;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateCategoryRequest> _createValidator;
    private readonly IValidator<UpdateCategoryRequest> _updateValidator;

    public CategoryService(ICategoryRepository repository, IMapper mapper,
        IValidator<CreateCategoryRequest> createValidator,
        IValidator<UpdateCategoryRequest> updateValidator)
    {
        _repository = repository;
        _mapper = mapper;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    public async Task<CategoryResponse> CreateAsync(CreateCategoryRequest request)
    {
        var validationResult = await _createValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var category = _mapper.Map<Category>(request);

        var createdCategory = await _repository.AddAsync(category);

        return _mapper.Map<CategoryResponse>(createdCategory);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var category = await _repository.GetByIdAsync(id);

        if (category is null)
        {
            return false;
        }

        if (await _repository.HasProductsAsync(id))
        {
            throw new BusinessRuleException("This category contains products and cannot be deleted.");
        }

        await _repository.DeleteAsync(category);

        return true;
    }

    public async Task<List<CategoryResponse>> GetAllAsync()
    {
        var categories = await _repository.GetAllAsync();

        return _mapper.Map<List<CategoryResponse>>(categories);
    }

    public async Task<CategoryResponse?> GetByIdAsync(int id)
    {
        var category = await _repository.GetByIdAsync(id);
        if (category == null)
        {
            return null;
        }

        return _mapper.Map<CategoryResponse>(category);
    }

    public async Task<CategoryResponse?> UpdateAsync(int id, UpdateCategoryRequest request)
    {
        var existingCategory = await _repository.GetByIdAsync(id);

        if (existingCategory is null) 
        {
            return null;
        }

        var validationResult = await _updateValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        _mapper.Map(request, existingCategory);

        await _repository.UpdateAsync(existingCategory);

        return _mapper.Map<CategoryResponse>(existingCategory);
    }
}
