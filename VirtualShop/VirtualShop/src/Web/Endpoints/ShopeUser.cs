using VirtualShop.Application.Common.Models;
using VirtualShop.Application.ShopUser.Commands.DeactivateUser;
using VirtualShop.Application.ShopUser.Commands.DeleteUser;
using VirtualShop.Application.ShopUser.Commands.LoginUser;
using VirtualShop.Application.ShopUser.Commands.RegisterUser;
namespace VirtualShop.Web.Endpoints;

public class ShopeUser : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapPost(RegisterUser, "Register")
            .MapPost(LoginUser,"login");

        app.MapGroup(this)
            .RequireAuthorization()
            .MapPut(UpdateUser, "{id}")
            .MapDelete(DeactivateUser, "Deactivate/{id}")
            .MapDelete(DeleteUser, "Delete/{id}");
    }
    public async Task<IResult> RegisterUser(ISender sender, RegisterUserCommand command)
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

    public async Task<IResult> LoginUser(ISender sender, LoginUserCommand command)
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
        return TypedResults.Problem("not emplemented", statusCode: StatusCodes.Status400BadRequest);
    }

    public async Task<IResult> DeactivateUser(ISender sender, string id )
    {
        var command = new DeactivateUserCommand() { UserId = id };
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
        var command = new DeleteUserCommand() { UserId = id };
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
