using Microsoft.AspNetCore.Authorization;

namespace VirtualShop.Infrastructure.Authorization.Orders;
public class OrdersRequirement : IAuthorizationRequirement
{
    public string RequiredPermission { get; }

    public OrdersRequirement(string requiredRole)
    {
        RequiredPermission = requiredRole;
    }
}
