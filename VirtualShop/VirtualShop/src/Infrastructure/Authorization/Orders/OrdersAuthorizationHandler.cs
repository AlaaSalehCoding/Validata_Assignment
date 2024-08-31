using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using VirtualShop.Application.Common.Repository;

namespace VirtualShop.Infrastructure.Authorization.Orders;

public class OrdersAuthorizationHandler : AuthorizationHandler<OrdersRequirement>
{
    private readonly ICommonRepository<Domain.Entities.Order> _orderRepo;

    public OrdersAuthorizationHandler(
        ICommonRepository<Domain.Entities.Order> orderRepo)
    {
        _orderRepo = orderRepo;
    }
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, OrdersRequirement requirement)
    {
        var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if ((requirement.RequiredPermission == OrdersPermissionConstants.CanUpdateOrder) ||
            (requirement.RequiredPermission == OrdersPermissionConstants.CanDeleteOrder) ||
            (requirement.RequiredPermission == OrdersPermissionConstants.CanGetOrder)
           )
        {
            if (context.Resource is long orderId)
            {
                var order = await _orderRepo.Get(o => o.Id == orderId, null, "Customer").FirstOrDefaultAsync();
                if (order?.Customer?.UserId == userId)
                {
                    context.Succeed(requirement);
                    return;
                }
            }
        }
        context.Fail();
        return;
    }
}
