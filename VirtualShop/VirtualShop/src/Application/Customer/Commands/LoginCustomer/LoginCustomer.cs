using VirtualShop.Application.Common.Interfaces;
using VirtualShop.Application.Common.Models;

namespace VirtualShop.Application.Customer.Commands.LoginUser;

public record LoginCustomerCommand : IRequest<Result>
{
    public string Username { get; set; } = null!;
    public string Password { get; set; }= null!;
}

public class LoginCustomerCommandValidator : AbstractValidator<LoginCustomerCommand>
{
    public LoginCustomerCommandValidator()
    {
    }
}

public class LoginCustomerCommandHandler : IRequestHandler<LoginCustomerCommand, Result>
{
    private readonly IIdentityService _identityService;

    public LoginCustomerCommandHandler( IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public Task<Result> Handle(LoginCustomerCommand request, CancellationToken cancellationToken)
    {
        return _identityService.Login(request.Username, request.Password);
    }
}
