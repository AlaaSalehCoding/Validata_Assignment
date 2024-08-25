using System.ComponentModel.DataAnnotations;
using VirtualShop.Application.Common.Interfaces;
using VirtualShop.Application.Common.Models;
using VirtualShop.Application.Common.Repository;
using VirtualShop.Domain.Constants;
using VirtualShop.Domain.Entities;

namespace VirtualShop.Application.ShopUser.Commands.RegisterUser;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result>
{
    private readonly IIdentityService _identityService;
    private readonly ICommoneRepository<Customer, long> _customerRepo;

    public RegisterUserCommandHandler(IIdentityService identityService, ICommoneRepository<Customer,long> customerRepo)
    {
        _identityService = identityService;
        _customerRepo = customerRepo;
    }

    public async Task<Result> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        (var result, var id) = await _identityService.CreateUserAsync(request.Email, request.Password, request.Username);
        if (result.Succeeded)
        {
            await _customerRepo.AddAsync(new Customer(0)            
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                PostalCode = request.PostalCode,
                Address = request.Address,
                UserId = id,
            });
            result = await _identityService.AddToRoleAsync(id, Roles.Customer);
        }
        return result;
    }
}
