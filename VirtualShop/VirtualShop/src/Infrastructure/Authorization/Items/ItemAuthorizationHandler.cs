using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using VirtualShop.Application.Common.Repository;
using VirtualShop.Application.Items.Commands.AddItemToOrder;

namespace VirtualShop.Infrastructure.Authorization.Items;

public class ItemAuthorizationHandler : AuthorizationHandler<ItemsRequirement>
{
    private readonly ICommonRepository<Domain.Entities.Order> _orderRepo;

    public ItemAuthorizationHandler(
        ICommonRepository<Domain.Entities.Order> orderRepo)
    {
        _orderRepo = orderRepo;
    }
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, ItemsRequirement requirement)
    {
        var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (requirement.RequiredPermission == ItemsPermissionConstants.CanAddItemToOrder)
        {
            if (context.Resource is AddItemToOrderCommand newItemData)
            {
                var order = await _orderRepo.Get(o=>o.Id==newItemData.OrderId,null, "Customer").FirstOrDefaultAsync();
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
