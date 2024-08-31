using CleanArchitecture.Application.Orders.Commands.CreateOrder;
using VirtualShop.Application.Orders.Commands.DeleteOrder;
using VirtualShop.Application.Orders.Commands.UpdateOrder;
using VirtualShop.Application.Orders.Queries.FilterOrders;
using VirtualShop.Application.Orders.Queries.GetOrder;
namespace VirtualShop.Web.Endpoints;

public class Order : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapPost(CreateOrder)
            .MapPut(UpdateOrder, "{id}")
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
    public async Task<IResult> UpdateOrder(ISender sender, string id, UpdateOrderCommand command)
    {
        var resault = await sender.Send(command);
        if (resault.Succeeded)
        {
            return Results.NoContent();
        }
        else
        {
            return Results.BadRequest(resault);
        }
    }
    public async Task<IResult> DeleteOrder(ISender sender, long id)
    {
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
    public async Task<IResult> FilterOrder(ISender sender, FilterOrdersQuery query)
    {
        var result = await sender.Send(query);
        return Results.Ok(result);
    }
    public async Task<IResult> GetOrder(ISender sender, long id)
    {
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
