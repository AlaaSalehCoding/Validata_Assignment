using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace VirtualShop.Infrastructure.Authorization;
public static partial class AuthorizationDependencyInjection
{
    public static IServiceCollection AddAuthorizationServices(this IServiceCollection services, IConfiguration configuration)
    {
        var handlers = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => typeof(IAuthorizationHandler).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

        foreach (var handler in handlers)
        {
            services.AddScoped(typeof(IAuthorizationHandler), handler);
        }
        return services;
    }
}
