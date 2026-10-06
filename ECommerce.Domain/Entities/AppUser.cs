using Microsoft.AspNetCore.Identity;

namespace ECommerce.Domain.Entities;

public class AppUser : IdentityUser
{
    public required string FullName { get; set; }
}
