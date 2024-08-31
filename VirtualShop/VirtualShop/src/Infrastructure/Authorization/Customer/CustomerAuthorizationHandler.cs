using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using VirtualShop.Application.Common.Interfaces;
using VirtualShop.Application.Common.Repository;
using VirtualShop.Application.Customer.Commands.UpdateUser;
using VirtualShop.Domain.Constants;

namespace VirtualShop.Infrastructure.Authorization.Customer;

public class CustomerAuthorizationHandler : AuthorizationHandler<CustomerRequirement>
{ 
    private readonly ICommonRepository<Domain.Entities.Customer> _customerRepo;

    public CustomerAuthorizationHandler( 
        ICommonRepository<Domain.Entities.Customer> customerRepo)
    { 
        _customerRepo = customerRepo;
    }
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, CustomerRequirement requirement)
    {
        var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (requirement.RequiredPermission == CustomerPermissionConstants.CanUpdateCustomer)
        {
            if (context.Resource is UpdateCustomerCommand customerUpdate)
            {
                var updatedCustomer = await _customerRepo.GetByIdAsync(customerUpdate.Id);
                if (context.User.IsInRole(Roles.Administrator) || updatedCustomer?.UserId == userId)
                {
                    context.Succeed(requirement);
                    return;
                }
            }
        }
        context.Fail();
        return ;
    }
}
