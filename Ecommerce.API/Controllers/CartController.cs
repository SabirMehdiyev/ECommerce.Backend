using System.Security.Claims;
using ECommerce.Application.DTOs.CartDtos;
using ECommerce.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrWhiteSpace(userId))
        {
            return Unauthorized();
        }

        return Ok(await _cartService.GetAsync(userId));
    }

    [HttpPost("items")]
    public async Task<IActionResult> Add(AddCartItemRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrWhiteSpace(userId))
        {
            return Unauthorized();
        }

        return Ok(await _cartService.AddAsync(userId, request));
    }

    [HttpPut("items/{productId}")]
    public async Task<IActionResult> Update(int productId, UpdateCartItemRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrWhiteSpace(userId))
        {
            return Unauthorized();
        }

        var cart = await _cartService.UpdateAsync(userId, productId, request);
        if (cart is null)
        {
            return NotFound();
        }

        return Ok(cart);
    }

    [HttpDelete("items/{productId}")]
    public async Task<IActionResult> Delete(int productId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrWhiteSpace(userId))
        {
            return Unauthorized();
        }

        if (!await _cartService.DeleteAsync(userId, productId))
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> Clear()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrWhiteSpace(userId))
        {
            return Unauthorized();
        }

        await _cartService.ClearAsync(userId);
        return NoContent();
    }
}
