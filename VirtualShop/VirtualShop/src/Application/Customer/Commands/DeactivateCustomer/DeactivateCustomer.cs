using VirtualShop.Application.Common.Interfaces;
using VirtualShop.Application.Common.Models;
using VirtualShop.Application.Common.Repository;
using VirtualShop.Application.Common.Security;
using VirtualShop.Domain.Constants;

namespace VirtualShop.Application.Customer.Commands.DeactivateUser;

[Authorize(Roles = Roles.Administrator)]
[Authorize(Policy = Policies.CanDeactivateUser)]
public record DeactivateCustomerCommand : IRequest<Result>
{
    public string UserId { get; set; } = null!;
}

public class DeactivateCustomerCommandValidator : AbstractValidator<DeactivateCustomerCommand>
{
    public DeactivateCustomerCommandValidator()
    {
    }
}

public class DeactivateCustomerCommandHandler : IRequestHandler<DeactivateCustomerCommand, Result>
{
    private readonly IIdentityService _identityService;
    private readonly ICommonRepository<Domain.Entities.Customer> _customerRepo;

    public DeactivateCustomerCommandHandler(
        IIdentityService identityService,
        ICommonRepository<Domain.Entities.Customer> customerRepo
        )
    {
        _identityService = identityService;
        _customerRepo = customerRepo;
    }

    public async Task<Result> Handle(DeactivateCustomerCommand request, CancellationToken cancellationToken)
    { 
        var result = Result.Success();

        var customer = await _customerRepo.Get().Where(c => c.UserId == request.UserId).FirstOrDefaultAsync();
        if (customer is not null)
        {
            return await _identityService.DeactivateUser(request.UserId);
        }
        return result;
    }
}
