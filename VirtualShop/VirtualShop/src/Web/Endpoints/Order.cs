using CleanArchitecture.Application.Order.Commands.CreateOrder;
using VirtualShop.Application.Order.Commands.DeleteOrder;
using VirtualShop.Application.Order.Commands.UpdateOrder;
using VirtualShop.Application.Order.Queries.FilterOrders;
using VirtualShop.Application.Order.Queries.GetOrder;
namespace VirtualShop.Web.Endpoints;

public class Order : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapPost(Create)
            .MapPut(Update, "{id}")
            .MapDelete(Delete, "{id}")
            .MapGet(Get, "{id}")
            .MapPost(Filter, "Filter");
    }
    public async Task<IResult> Create(ISender sender, CreateOrderCommand command)
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
    public async Task<IResult> Update(ISender sender, string id, UpdateOrderCommand command)
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
    public async Task<IResult> Delete(ISender sender, long id)
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
    public async Task<IResult> Filter(ISender sender, FilterOrdersQuery query)
    {
        var result = await sender.Send(query);
        return Results.Ok(result);
    }
    public async Task<IResult> Get(ISender sender, string id)
    {
        var query = new GetOrderQuery();
        var result = await sender.Send(query);
        return Results.Ok(result);
    }
}
