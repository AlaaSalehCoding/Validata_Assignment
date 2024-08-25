using VirtualShop.Application.Common.Interfaces;
using VirtualShop.Application.Common.Models;
using VirtualShop.Application.Common.Security;
using VirtualShop.Application.ShopUser.Commands.DeactivateUser;
using VirtualShop.Domain.Constants;

namespace VirtualShop.Application.ShopUser.Commands.DeleteUser;

[Authorize(Roles = Roles.Administrator)]
[Authorize(Policy = Policies.CanDeleteUser)]
public record DeleteUserCommand : IRequest<Result>
{
    public string UserId { get; set; } = null!;
}

public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator()
    {
    }
}

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Result>
{
    private readonly IIdentityService _identityService;

    public DeleteUserCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        return await _identityService.DeleteUserAsync(request.UserId);
    }
}
