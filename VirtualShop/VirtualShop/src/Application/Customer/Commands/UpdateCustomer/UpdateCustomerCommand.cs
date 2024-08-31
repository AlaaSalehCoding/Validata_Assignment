using VirtualShop.Application.Common.Models;

namespace VirtualShop.Application.Customer.Commands.UpdateUser;

public record UpdateCustomerCommand : IRequest<Result>
{
    public long Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
}
