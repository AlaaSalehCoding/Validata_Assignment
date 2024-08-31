using System.Net;
using CleanArchitecture.Application.Orders.Commands.CreateOrder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using VirtualShop.Application.Orders.Commands.DeleteOrder;
using VirtualShop.Application.Orders.Commands.UpdateOrder;
using VirtualShop.Application.Orders.Queries.FilterOrders;
using VirtualShop.Application.Orders.Queries.GetOrder;
using VirtualShop.Infrastructure.Authorization.Items;
using VirtualShop.Infrastructure.Authorization.Orders;
namespace VirtualShop.Web.Endpoints;

public class Order : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapPost(CreateOrder)
            // .MapPut(UpdateOrder, "{id}")  //no business need for updating order
            .MapDelete(DeleteOrder, "{id}")
            .MapGet(GetOrder, "{id}")
            .MapPost(FilterOrder, "Filter");
    }
    public async Task<IResult> CreateOrder(ISender sender, CreateOrderCommand command)
    {
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
    public async Task<IResult> UpdateOrder(
        IAuthorizationService authorization,
        IHttpContextAccessor httpContextAccessor,
        ISender sender,
        long id,
        UpdateOrderCommand command)
    {
        var user = httpContextAccessor?.HttpContext?.User;
        if (user is null)
        {
            return TypedResults.Problem("Unauthorized", statusCode: StatusCodes.Status403Forbidden);
        }
        var requirement = new OrdersRequirement(OrdersPermissionConstants.CanUpdateOrder);
        var authorizationResult = await authorization.AuthorizeAsync(user, id, requirement);
        if (!authorizationResult.Succeeded)
        {
            return TypedResults.Problem("Unauthorized", statusCode: StatusCodes.Status403Forbidden);
        }
        var result = await sender.Send(command);

        if (result.Succeeded)
        {
            return Results.NoContent();
        }
        else
        {
            return Results.BadRequest(result);
        }
    }
    public async Task<IResult> DeleteOrder(
        IAuthorizationService authorization,
        IHttpContextAccessor httpContextAccessor,
        ISender sender,
        long id)
    {
        var user = httpContextAccessor?.HttpContext?.User;
        if (user is null)
        {
            return TypedResults.Problem("Unauthorized", statusCode: StatusCodes.Status403Forbidden);
        }
        var requirement = new OrdersRequirement(OrdersPermissionConstants.CanDeleteOrder);
        var authorizationResult = await authorization.AuthorizeAsync(user, id, requirement);
        if (!authorizationResult.Succeeded)
        {
            return TypedResults.Problem("Unauthorized", statusCode: StatusCodes.Status403Forbidden);
        }

        var command = new DeleteOrderCommand() { Id = id };
        var result = await sender.Send(command);
        if (result.Succeeded)
        {
            return Results.NoContent();
        }
        else
        {
            return Results.BadRequest(result);
        }
    }
    public async Task<IResult> FilterOrder(
        ISender sender,
        FilterOrdersQuery query)
    {
        var result = await sender.Send(query);
        return Results.Ok(result);
    }
    public async Task<IResult> GetOrder(
        IAuthorizationService authorization,
        IHttpContextAccessor httpContextAccessor,
        ISender sender,
        long id)
    {
        var user = httpContextAccessor?.HttpContext?.User;
        if (user is null)
        {
            return TypedResults.Problem("Unauthorized", statusCode: StatusCodes.Status403Forbidden);
        }
        var requirement = new OrdersRequirement(OrdersPermissionConstants.CanGetOrder);
        var authorizationResult = await authorization.AuthorizeAsync(user, id, requirement);
        if (!authorizationResult.Succeeded)
        {
            return TypedResults.Problem("Unauthorized", statusCode: StatusCodes.Status403Forbidden);
        }
        var query = new GetOrderQuery() { Id = id };
        var result = await sender.Send(query);
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
