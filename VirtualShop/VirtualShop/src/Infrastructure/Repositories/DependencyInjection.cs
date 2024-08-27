using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VirtualShop.Application.Common.Repository;
using VirtualShop.Application.Product;
using VirtualShop.Infrastructure.Repositories;

namespace Microsoft.Extensions.DependencyInjection;
public static partial class DependencyInjection
{
    public static IServiceCollection AddRepositoryServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped(typeof(ICommonRepository<>), typeof(CommoneRepository<>));

        services.AddScoped<IProductRepository, ProductRepository>();


        return services;
    }
}
