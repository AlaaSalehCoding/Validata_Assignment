using System.ComponentModel.DataAnnotations;
using VirtualShop.Application.Common.Interfaces;
using VirtualShop.Application.Common.Models;
using VirtualShop.Application.Common.Repository;
using VirtualShop.Domain.Constants;
using VirtualShop.Domain.Entities;

namespace VirtualShop.Application.Customer.Commands.RegisterUser;

public class RegisterCustomerCommandHandler : IRequestHandler<RegisterCustomerCommand, Result>
{
    private readonly IIdentityService _identityService;
    private readonly ICommonRepository<Domain.Entities.Customer> _customerRepo;

    public RegisterCustomerCommandHandler(IIdentityService identityService, ICommonRepository<Domain.Entities.Customer > customerRepo)
    {
        _identityService = identityService;
        _customerRepo = customerRepo;
    }

    public async Task<Result> Handle(RegisterCustomerCommand request, CancellationToken cancellationToken)
    {
        (var result, var id) = await _identityService.CreateUserAsync(request.Email, request.Password, request.Username);
        if (result.Succeeded)
        {
            await _customerRepo.AddAsync(new Domain.Entities.Customer(0)            
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
