using System.ComponentModel.DataAnnotations;
using VirtualShop.Application.Common.Interfaces;
using VirtualShop.Application.Common.Models;

namespace VirtualShop.Application.ShopUser.Commands.RegisterUser;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result>
{
    private readonly IIdentityService _identityService;

    public RegisterUserCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Result> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        (var result, var id) = await _identityService.CreateUserAsync(request.Email, request.Password,
            request.Username, request.FirstName, request.LastName, request.Address, request.PostalCode);

        return result;
    }
}
