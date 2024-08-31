using VirtualShop.Application.Common.Interfaces;
using VirtualShop.Application.Common.Repository;
using VirtualShop.Domain.Constants;

namespace VirtualShop.Application.Customer.Commands.UpdateUser;

public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
{ 
    public UpdateCustomerCommandValidator( )
    { 
        RuleFor(p => p.Id)
             .GreaterThan(0);
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
}
