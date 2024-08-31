using Microsoft.AspNetCore.Authorization;

namespace VirtualShop.Infrastructure.Authorization.Items;
public class ItemsRequirement : IAuthorizationRequirement
{
    public string RequiredPermission { get; }

    public ItemsRequirement(string requiredRole)
    {
        RequiredPermission = requiredRole;
    }
}
