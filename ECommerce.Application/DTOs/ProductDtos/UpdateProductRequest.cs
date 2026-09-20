namespace ECommerce.Application.DTOs.ProductDtos;

public class UpdateProductRequest
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public int CategoryId { get; set; }
}
