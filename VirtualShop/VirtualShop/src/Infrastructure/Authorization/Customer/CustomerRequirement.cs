using Microsoft.AspNetCore.Authorization;

namespace VirtualShop.Infrastructure.Authorization.Customer;

public class CustomerRequirement : IAuthorizationRequirement
{
    public string RequiredPermission { get; }

    public CustomerRequirement(string requiredRole)
    {
        RequiredPermission = requiredRole;
    }
}
