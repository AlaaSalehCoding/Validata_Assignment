using VirtualShop.Application.Common.Interfaces;
using VirtualShop.Application.Common.Repository;
using VirtualShop.Domain.Constants;

namespace VirtualShop.Application.Customer.Commands.UpdateUser;

public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
{
    private readonly IIdentityService _identityService;
    private readonly IUser _user;
    private readonly ICommonRepository<Domain.Entities.Customer> _customerRepo;

    public UpdateCustomerCommandValidator(
        IIdentityService identityService,
        IUser user,
        ICommonRepository<Domain.Entities.Customer> customerRepo
        )
    {
        _identityService = identityService;
        _user = user;
        _customerRepo = customerRepo;

        RuleFor(p => p.Id)
             .GreaterThan(0)
             .MustAsync(BeUniqueUserName).WithMessage("You don't have access to this Item.");
        RuleFor(v => v.FirstName)
            .MinimumLength(1)
            .MaximumLength(50)
            .NotEmpty();
        RuleFor(v => v.LastName)
            .MinimumLength(4)
            .MaximumLength(50)
            .NotEmpty();
        RuleFor(v => v.Address)
            .MinimumLength(3)
            .MaximumLength(250)
            .NotEmpty();
        RuleFor(v => v.PostalCode)
            .NotEmpty()
            .Matches(@"^\d{5}(-\d{4})?$")
            .WithMessage("Postal Code is not valid.");
    }
    private async Task<bool> BeUniqueUserName(long id, CancellationToken cancellationToken)
    {
        var updatedCustomer = await _customerRepo.GetByIdAsync(id);
        if (updatedCustomer == null) { return false; }

        if (await _identityService.IsInRoleAsync(_user.Id ?? "", Roles.Administrator)) { return true; }

        if (updatedCustomer.UserId == _user.Id) { return true; }

        return false;

    }
}
