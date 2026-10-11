using AutoMapper;
using ECommerce.Application.DTOs.CartDtos;
using ECommerce.Application.Exceptions;
using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Application.Interfaces.Services;
using ECommerce.Domain.Entities;
using FluentValidation;

namespace ECommerce.Application.Implementations.Services;

public class CartService : ICartService
{
    private readonly ICartRepository _repository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<AddCartItemRequest> _addValidator;
    private readonly IValidator<UpdateCartItemRequest> _updateValidator;

    public CartService(
        ICartRepository repository,
        IProductRepository productRepository,
        IMapper mapper,
        IValidator<AddCartItemRequest> addValidator,
        IValidator<UpdateCartItemRequest> updateValidator)
    {
        _repository = repository;
        _productRepository = productRepository;
        _mapper = mapper;
        _addValidator = addValidator;
        _updateValidator = updateValidator;
    }

    public async Task<CartResponse> GetAsync(string userId)
    {
        var items = await _repository.GetAllAsync(userId);

        return new CartResponse
        {
            Items = _mapper.Map<List<CartItemResponse>>(items)
        };
    }

    public async Task<CartResponse> AddAsync(
        string userId, AddCartItemRequest request)
    {
        var validationResult = await _addValidator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var product = await _productRepository.GetByIdAsync(request.ProductId);

        if (product is null)
        {
            throw new BusinessRuleException(
                "The selected product does not exist.");
        }

        var item = await _repository.GetItemAsync(userId, request.ProductId);
        var quantity = (long)(item?.Quantity ?? 0) + request.Quantity;

        if (quantity > product.Stock)
        {
            throw new BusinessRuleException(
                "The requested quantity exceeds the available stock.");
        }

        if (item is null)
        {
            await _repository.AddAsync(new CartItem
            {
                UserId = userId,
                ProductId = request.ProductId,
                Quantity = (int)quantity
            });
        }
        else
        {
            item.Quantity = (int)quantity;

            await _repository.UpdateAsync(item);
        }

        return await GetAsync(userId);
    }

    public async Task<CartResponse?> UpdateAsync(
        string userId, int productId, UpdateCartItemRequest request)
    {
        var validationResult = await _updateValidator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var item = await _repository.GetItemAsync(userId, productId);

        if (item is null)
        {
            return null;
        }

        var product = await _productRepository.GetByIdAsync(productId);

        if (product is null)
        {
            throw new BusinessRuleException(
                "The selected product does not exist.");
        }

        if (request.Quantity > product.Stock)
        {
            throw new BusinessRuleException(
                "The requested quantity exceeds the available stock.");
        }

        item.Quantity = request.Quantity;

        await _repository.UpdateAsync(item);

        return await GetAsync(userId);
    }

    public Task<bool> DeleteAsync(string userId, int productId)
    {
        return _repository.DeleteAsync(userId, productId);
    }

    public Task ClearAsync(string userId)
    {
        return _repository.ClearAsync(userId);
    }
}