using VirtualShop.Application.Common.Models;
using VirtualShop.Application.Customer.Commands.DeactivateUser;
using VirtualShop.Application.Customer.Commands.DeleteUser;
using VirtualShop.Application.Customer.Commands.LoginUser;
using VirtualShop.Application.Customer.Commands.RegisterUser;
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
