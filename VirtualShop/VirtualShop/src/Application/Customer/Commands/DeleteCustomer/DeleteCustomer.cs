using VirtualShop.Application.Common.Interfaces;
using VirtualShop.Application.Common.Models;
using VirtualShop.Application.Common.Repository;

namespace VirtualShop.Application.Customer.Commands.DeleteUser;

public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, Result>
{
    private readonly IIdentityService _identityService;
    private readonly ICommonRepository<Domain.Entities.Customer> _customerRepo;

    public DeleteCustomerCommandHandler(
        IIdentityService identityService, 
        ICommonRepository<Domain.Entities.Customer> customerRepo
        )
    {
        _identityService = identityService;
        _customerRepo = customerRepo;
    }

    public async Task<Result> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var result = Result.Success();

        var customer = await _customerRepo.Get().Where(c=>c.UserId == request.UserId).FirstOrDefaultAsync();
        if (customer is not null)
        {
            result = await _identityService.DeleteUserAsync(request.UserId);
            await _customerRepo.DeleteAsync(customer); 
        }
        return result;
    }
}
