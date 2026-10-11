namespace ECommerce.Application.DTOs.CartDtos;

public class CartResponse
{
    public List<CartItemResponse> Items { get; set; } = new();
    public decimal TotalAmount => Items.Sum(item => item.LineTotal);
}
