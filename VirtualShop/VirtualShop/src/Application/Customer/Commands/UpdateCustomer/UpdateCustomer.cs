using VirtualShop.Application.Common.Interfaces;
using VirtualShop.Application.Common.Models;
using VirtualShop.Application.Common.Repository;

namespace VirtualShop.Application.Customer.Commands.UpdateUser;

public record UpdateCustomerCommand : IRequest<Result>
{
    public long Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
}

public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
{
    public UpdateCustomerCommandValidator()
    {
        //Attribute validator 
        //Authorization
    }
}

public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Result>
{ 
    private readonly ICommonRepository<Domain.Entities.Customer> _customerRepo;
    public UpdateCustomerCommandHandler(ICommonRepository<Domain.Entities.Customer> customerRepo)
    {
        _customerRepo = customerRepo;
    }

    public async Task<Result> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepo.GetById(id: request.Id);
        if (customer == null) {
            return Result.Failure(["No such customer"]);
        }
        customer.FirstName = request.FirstName;
        customer.LastName = request.LastName;
        customer.Address = request.Address;
        customer.PostalCode = request.PostalCode;

        await _customerRepo.SaveChangesAsync();

        return Result.Success();
    }
}
