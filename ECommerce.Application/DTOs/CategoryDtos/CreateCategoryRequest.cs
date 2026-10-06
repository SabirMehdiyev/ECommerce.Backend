namespace ECommerce.Application.DTOs.CategoryDtos;

public class CreateCategoryRequest
{
    public required string Name { get; set; }
    public string? Description { get; set; }
}
