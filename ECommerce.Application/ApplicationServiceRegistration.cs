using ECommerce.Application.Implementations.Services;
using ECommerce.Application.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ECommerce.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg => { }, Assembly.GetExecutingAssembly());
        services.AddScoped<IProductService, ProductService>();
        return services;
    }
}
