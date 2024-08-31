using VirtualShop.Application.Common.Models;
using VirtualShop.Application.Common.Security;
using VirtualShop.Domain.Constants;

namespace VirtualShop.Application.Customer.Commands.DeleteUser;

[Authorize(Roles = Roles.Administrator)]
[Authorize(Policy = Policies.CanDeleteUser)]
public record DeleteCustomerCommand : IRequest<Result>
{
    public string UserId { get; set; } = null!;
}
