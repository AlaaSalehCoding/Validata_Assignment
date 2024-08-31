using VirtualShop.Application.Product.Commands.CreateProduct;
using VirtualShop.Application.Product.Commands.DeleteProduct;
using VirtualShop.Application.Product.Commands.UpdateProduct;
using VirtualShop.Application.Product.Queries.FilterProducts;
using VirtualShop.Application.Product.Queries.GetProduct;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
namespace VirtualShop.Web.Endpoints;

public class Product : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapPost(CreateProduct)
            .MapPut(UpdateProduct, "{id}")
            .MapDelete(DeleteProduct, "{id}")
            .MapGet(GetProduct, "{id}")
            .MapPost(FilterProduct, "Filter");
    }
    public async Task<IResult> CreateProduct(ISender sender, CreateProductCommand command)
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
    public async Task<IResult> UpdateProduct(ISender sender, string id, UpdateProductCommand command)
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

    public async Task<IResult> DeleteProduct(ISender sender, long id)
    {
        var command = new DeleteProductCommand() { Id = id };
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
    public async Task<IResult> GetProduct(ISender sender, long id)
    {
        var query = new GetProductQuery() { Id = id };
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
    public async Task<IResult> FilterProduct(ISender sender, FilterProductsQuery query)
    {
        var result = await sender.Send(query);
        return Results.Ok(result);
    }
}
