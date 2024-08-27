using VirtualShop.Application.Common.Models;
using VirtualShop.Application.Customer.Commands.DeactivateUser;
using VirtualShop.Application.Customer.Commands.DeleteUser;
using VirtualShop.Application.Customer.Commands.LoginUser;
using VirtualShop.Application.Customer.Commands.RegisterUser;
using VirtualShop.Application.Product.Commands.CreateProduct;
using VirtualShop.Application.Product.Commands.DeleteProduct;
using VirtualShop.Application.Product.Commands.UpdateProduct;
using VirtualShop.Application.Product.Queries.GetProducts;
namespace VirtualShop.Web.Endpoints;

public class Customer : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapPost(RegisterUser, "Register")
            .MapPost(LoginUser, "login");

        app.MapGroup(this)
            .RequireAuthorization()
            .MapPut(UpdateUser, "{id}")
            .MapDelete(DeactivateUser, "Deactivate/{id}")
            .MapDelete(DeleteUser, "Delete/{id}");
    }
    public async Task<IResult> RegisterUser(ISender sender, RegisterCustomerCommand command)
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

    public async Task<IResult> LoginUser(ISender sender, LoginCustomerCommand command)
    {
        var resault = await sender.Send(command);
        if (resault.Succeeded)
        {
            return TypedResults.Empty;
        }
        else
        {
            return TypedResults.Problem(resault.Errors[0], statusCode: StatusCodes.Status401Unauthorized);
        }
    }

    public IResult UpdateUser(ISender sender, string id, dynamic command)
    {
        return TypedResults.Problem("not implemented", statusCode: StatusCodes.Status400BadRequest);
    }

    public async Task<IResult> DeactivateUser(ISender sender, string id)
    {
        var command = new DeactivateCustomerCommand() { UserId = id };
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
    public async Task<IResult> DeleteUser(ISender sender, string id)
    {
        var command = new DeleteCustomerCommand() { UserId = id };
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


public class Product : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapPost(Create)
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

    public Task<ProductsVm> GetTodoLists(ISender sender, GetProductsQuery query)
    {
        return sender.Send(query);
    }
}
