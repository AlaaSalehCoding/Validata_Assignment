using CleanArchitecture.Application.Orders.Commands.CreateOrder;
using VirtualShop.Application.Items.Commands.AddItemToOrder;
using VirtualShop.Application.Orders.Commands.DeleteOrder;
using VirtualShop.Application.Orders.Commands.UpdateOrder;
using VirtualShop.Application.Orders.Queries.FilterOrders;
using VirtualShop.Application.Orders.Queries.GetOrder;
namespace VirtualShop.Web.Endpoints;

public class Item : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapPost(AddItemToProduct) ;
    }
    public async Task<IResult> AddItemToProduct(ISender sender, AddItemToOrderCommand command)
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
}
