using VirtualShop.Application.Product.Commands.CreateProduct;
using VirtualShop.Application.Product.Commands.DeleteProduct;
using VirtualShop.Application.Product.Commands.UpdateProduct;
using VirtualShop.Application.Product.Queries.FilterProducts;
namespace VirtualShop.Web.Endpoints;

public class Product : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapPost(Create)
            .MapPost(Filter,"Filter")
            .MapPut(Update, "{id}")
            .MapDelete(Delete, "{id}");
    }
    public async Task<IResult> Create(ISender sender, CreateProductCommand command)
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
    public async Task<IResult> Filter(ISender sender, FilterProductsQuery query)
    {
        var result =await sender.Send(query);
        return Results.Ok(result);
    }
    

    public async Task<IResult> Update(ISender sender, string id, UpdateProductCommand command)
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
        var command = new DeleteProductCommand() { Id = id };
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
}
