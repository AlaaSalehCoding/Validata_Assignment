using VirtualShop.Application.Common.Models;
using VirtualShop.Application.ShopUser.Commands.LoginUser;
using VirtualShop.Application.ShopUser.Commands.RegisterUser;
using VirtualShop.Application.TodoItems.Commands.UpdateTodoItem;

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
            .MapDelete(DeleteUser, "{id}");
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

    public IResult UpdateUser(ISender sender, int id, dynamic command)
    {
        return TypedResults.Problem("not emplemented", statusCode: StatusCodes.Status400BadRequest);
    }

    public IResult DeleteUser(ISender sender, int id )
    {
        //need to consider soft delete
        return TypedResults.Problem("not emplemented", statusCode: StatusCodes.Status400BadRequest);
    }
}
