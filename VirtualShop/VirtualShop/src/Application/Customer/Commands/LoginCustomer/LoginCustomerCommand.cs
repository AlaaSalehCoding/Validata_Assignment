using VirtualShop.Application.Common.Models;

namespace VirtualShop.Application.Customer.Commands.LoginUser;

public record LoginCustomerCommand : IRequest<Result>
{
    public string Username { get; set; } = null!;
    public string Password { get; set; }= null!;
}
