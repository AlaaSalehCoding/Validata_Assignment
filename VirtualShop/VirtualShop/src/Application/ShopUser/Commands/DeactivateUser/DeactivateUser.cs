using VirtualShop.Application.Common.Interfaces;
using VirtualShop.Application.Common.Models;
using VirtualShop.Application.Common.Security;
using VirtualShop.Domain.Constants;

namespace VirtualShop.Application.ShopUser.Commands.DeactivateUser;

[Authorize(Roles = Roles.Administrator)]
[Authorize(Policy = Policies.CanDeactivateUser)]
public record DeactivateUserCommand : IRequest<Result>
{
    public string UserId { get; set; } = null!;
}

public class DeactivateUserCommandValidator : AbstractValidator<DeactivateUserCommand>
{
    public DeactivateUserCommandValidator()
    {
    }
}

public class DeactivateUserCommandHandler : IRequestHandler<DeactivateUserCommand, Result>
{
    private readonly IIdentityService _identityService;

    public DeactivateUserCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Result> Handle(DeactivateUserCommand request, CancellationToken cancellationToken)
    {
        return await _identityService.DeactivateUser(request.UserId);
    }
}
