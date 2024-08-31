using VirtualShop.Application.Common.Interfaces;
using VirtualShop.Application.Common.Models;

namespace VirtualShop.Application.Customer.Commands.LoginUser;

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
