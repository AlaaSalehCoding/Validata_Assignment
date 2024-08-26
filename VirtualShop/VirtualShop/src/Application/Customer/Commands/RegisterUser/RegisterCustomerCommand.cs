using VirtualShop.Application.Common.Models;

namespace VirtualShop.Application.Customer.Commands.RegisterUser;

public record RegisterCustomerCommand : IRequest<Result>
{
    public required string Email { get; init; }
    public required string Password { get; init; }
    public string Username { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
}
