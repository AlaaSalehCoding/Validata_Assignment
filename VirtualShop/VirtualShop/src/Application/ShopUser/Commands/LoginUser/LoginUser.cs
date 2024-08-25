using VirtualShop.Application.Common.Interfaces;
using VirtualShop.Application.Common.Models;

namespace VirtualShop.Application.ShopUser.Commands.LoginUser;

public record LoginUserCommand : IRequest<Result>
{
    public string Username { get; set; } = null!;
    public string Password { get; set; }= null!;
}

public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
    }
}

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Result>
{
    private readonly IIdentityService _identityService;

    public LoginUserCommandHandler( IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public Task<Result> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        return _identityService.Login(request.Username, request.Password);
    }
}
