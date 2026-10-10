namespace ECommerce.Application.DTOs.ProductDtos;

public class ProductResponse
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public int CategoryId { get; set; }
    public required string CategoryName { get; set; } 
}
