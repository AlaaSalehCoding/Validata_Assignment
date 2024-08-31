using VirtualShop.Application.Common.Models;
using VirtualShop.Application.Common.Security;
using VirtualShop.Domain.Constants;

namespace VirtualShop.Application.Customer.Commands.DeactivateUser;

[Authorize(Roles = Roles.Administrator)]
[Authorize(Policy = Policies.CanDeactivateUser)]
public record DeactivateCustomerCommand : IRequest<Result>
{
    public string UserId { get; set; } = null!;
}
