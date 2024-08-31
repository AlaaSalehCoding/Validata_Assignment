using Microsoft.AspNetCore.Authorization;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VirtualShop.Application.Common.Repository;
using VirtualShop.Application.Products;
using VirtualShop.Infrastructure.Repositories;

namespace Microsoft.Extensions.DependencyInjection;
public static partial class DependencyInjection
{
    public static IServiceCollection AddRepositoryServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped(typeof(ICommonRepository<>), typeof(CommoneRepository<>));
        //services.AddScoped<IProductRepository, ProductRepository>();

        var commonRepoType = typeof(ICommonRepository<>); 
        var assembly = Assembly.GetExecutingAssembly();

        // Find all types that implement ICommonRepository<>
        var repositoryTypes = assembly.GetTypes()
            .Where(type => !type.IsInterface && !type.IsAbstract)
            .SelectMany(type => type.GetInterfaces(), (type, iface) => new { type, iface })
            .Where(t => t.iface.IsGenericType && t.iface.GetGenericTypeDefinition() == commonRepoType)
            .Select(t => t.type)
            .Distinct();

        // Register each repository type as a scoped service
        foreach (var repositoryType in repositoryTypes)
        {
            var matchingInterface = repositoryType.GetInterfaces()
                .FirstOrDefault(interfaceItem =>
                    interfaceItem.GetInterfaces().Any(subInterfaceItem =>
                        subInterfaceItem.IsGenericType && subInterfaceItem.GetGenericTypeDefinition() == commonRepoType));

            if (matchingInterface != null)
            {
                services.AddScoped(matchingInterface, repositoryType);
            }
        }
        return services;
    }
}
