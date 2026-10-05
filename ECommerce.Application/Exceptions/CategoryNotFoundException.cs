namespace ECommerce.Application.Exceptions;

public class CategoryNotFoundException : Exception
{
    public CategoryNotFoundException() : base("The selected category does not exist.")
    {
    }
}
