using VirtualShop.Application.Common.Interfaces;

namespace VirtualShop.Application.Customer.Commands.RegisterUser;

public class RegisterCustomerCommandValidator : AbstractValidator<RegisterCustomerCommand>
{
    private readonly IIdentityService _identityService;
    public RegisterCustomerCommandValidator(IIdentityService identityService)
    {
        _identityService = identityService;
        RuleFor(v => v.Password)
            .NotEmpty()
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
            .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches(@"[0-9]").WithMessage("Password must contain at least one number.")
            .Matches(@"[!@#$%^&*()_+{}\[\]:;,.<>?/\|\\~`-]").WithMessage("Password must contain at least one special character (!@#$%^&*()_+{}[]:;,.<>?/\\|\\~`-).");

        RuleFor(v => v.Email)
            .NotEmpty()
            .EmailAddress()
            .MustAsync(BeUniqueEmail).WithMessage("Email already exists.");

        RuleFor(v => v.Username)
             .NotEmpty()
             .MinimumLength(4)
             .MaximumLength(50)
             .MustAsync(BeUniqueUserName).WithMessage("Username already exists.");
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
    private async Task<bool> BeUniqueUserName(string userName, CancellationToken cancellationToken)
    {
        return await _identityService.isUniqueUsername(userName);

    }
    private async Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken)
    {
        return await _identityService.isUniqueEmail(email);

    }
}
