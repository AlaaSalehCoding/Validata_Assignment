using Microsoft.AspNetCore.Authorization;
using VirtualShop.Application.Customer.Commands.DeactivateUser;
using VirtualShop.Application.Customer.Commands.DeleteUser;
using VirtualShop.Application.Customer.Commands.LoginUser;
using VirtualShop.Application.Customer.Commands.RegisterUser;
using VirtualShop.Application.Customer.Commands.UpdateUser;
using VirtualShop.Infrastructure.Authorization.Customer;
namespace VirtualShop.Web.Endpoints;

public class Customer : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapPost(RegisterCustomer, "Register")
            .MapPost(LoginCustomer, "login");

        app.MapGroup(this)
            .RequireAuthorization()
            .MapPut(UpdateCustomer, "{id}")
            .MapDelete(DeactivateCustomer, "Deactivate/{id}")
            .MapDelete(DeleteCustomer, "Delete/{id}");
    }
    public async Task<IResult> RegisterCustomer(ISender sender, RegisterCustomerCommand command)
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

    public async Task<IResult> LoginCustomer(ISender sender, LoginCustomerCommand command)
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

    public async Task<IResult> UpdateCustomer(
        IAuthorizationService authorization,
        IHttpContextAccessor httpContextAccessor,
        ISender sender, 
        long id,
        UpdateCustomerCommand command)
    {
        var user = httpContextAccessor?.HttpContext?.User;
        if (user is null)
        {
            return TypedResults.Problem("Unauthorized", statusCode: StatusCodes.Status403Forbidden);
        }
        var requirement = new CustomerRequirement(CustomerPermissionConstants.CanUpdateCustomer);
        var authorizationResult = await authorization.AuthorizeAsync(user, command, requirement);
        if (!authorizationResult.Succeeded)
        {
            return TypedResults.Problem("Unauthorized", statusCode: StatusCodes.Status403Forbidden);
        }
        var resault = await sender.Send(command);
        if (resault.Succeeded)
        {
            return TypedResults.Empty;
        }
        return TypedResults.Problem(resault.Errors[0], statusCode: StatusCodes.Status500InternalServerError);
    }

    public async Task<IResult> DeactivateCustomer(ISender sender, string id)
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
    public async Task<IResult> DeleteCustomer(ISender sender, string id)
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
