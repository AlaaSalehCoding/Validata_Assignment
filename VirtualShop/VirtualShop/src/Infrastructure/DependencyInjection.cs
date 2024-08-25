using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using VirtualShop.Application.Common.Interfaces;
using VirtualShop.Application.Common.Repository;
using VirtualShop.Application.Common.Uow;
using VirtualShop.Domain.Constants;
using VirtualShop.Infrastructure.Data;
using VirtualShop.Infrastructure.Data.Interceptors;
using VirtualShop.Infrastructure.Identity;
using VirtualShop.Infrastructure.Repositories;
using VirtualShop.Infrastructure.Uow;

namespace Microsoft.Extensions.DependencyInjection;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        Guard.Against.Null(connectionString, message: "Connection string 'DefaultConnection' not found.");

        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());

            options.UseSqlServer(connectionString);
        });

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<ApplicationDbContextInitialiser>();

        services.AddAuthentication()
            .AddBearerToken(IdentityConstants.BearerScheme);

        services.AddAuthorizationBuilder();

        services
            .AddIdentityCore<ApplicationUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddApiEndpoints();

        services.AddSingleton(TimeProvider.System);
        services.AddTransient<IIdentityService, IdentityService>();

        services.AddScoped(typeof(ICommoneRepository<,>), typeof(CommoneRepository<,>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddAuthorization(options =>
            options.AddPolicy(Policies.CanDeleteUser, policy => policy.RequireRole(Roles.Administrator)));
        services.AddAuthorization(options =>
            options.AddPolicy(Policies.CanDeactivateUser, policy => policy.RequireRole(Roles.Administrator)));

        return services;
    }
}
