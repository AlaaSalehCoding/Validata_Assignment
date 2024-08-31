using System.Net;
using CleanArchitecture.Application.Orders.Commands.CreateOrder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using VirtualShop.Application.Items.Commands.AddItemToOrder;
using VirtualShop.Application.Orders.Commands.DeleteOrder;
using VirtualShop.Application.Orders.Commands.UpdateOrder;
using VirtualShop.Application.Orders.Queries.FilterOrders;
using VirtualShop.Application.Orders.Queries.GetOrder;
using VirtualShop.Infrastructure.Authorization.Customer;
using VirtualShop.Infrastructure.Authorization.Items;
namespace VirtualShop.Web.Endpoints;

public class Item : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapPost(AddItemToProduct);
    }
    public async Task<IResult> AddItemToProduct(
        IAuthorizationService authorization,
        IHttpContextAccessor httpContextAccessor,
        ISender sender,
        AddItemToOrderCommand command)
    {
        var user = httpContextAccessor?.HttpContext?.User;
        if (user is null)
        {
            return TypedResults.Problem("Unauthorized", statusCode: StatusCodes.Status403Forbidden);
        }
        var requirement = new ItemsRequirement(ItemsPermissionConstants.CanAddItemToOrder);
        var authorizationResult = await authorization.AuthorizeAsync(user, command, requirement);
        if (!authorizationResult.Succeeded)
        {
            return TypedResults.Problem("Unauthorized", statusCode: StatusCodes.Status403Forbidden);
        }
        var result = await sender.Send(command);
        if (result.Succeeded)
        {
            return Results.Ok(result.SuccessStatus);
        }
        else
        {
            return Results.BadRequest(result);
        }
    }
}
