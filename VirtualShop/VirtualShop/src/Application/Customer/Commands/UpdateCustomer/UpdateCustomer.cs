﻿using FluentValidation;
using VirtualShop.Application.Common.Models;
using VirtualShop.Application.Common.Repository;

namespace VirtualShop.Application.Customer.Commands.UpdateUser;

public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Result>
{
    private readonly ICommonRepository<Domain.Entities.Customer> _customerRepo;
    public UpdateCustomerCommandHandler(ICommonRepository<Domain.Entities.Customer> customerRepo)
    {
        _customerRepo = customerRepo;
    }

    public async Task<Result> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepo.GetByIdAsync(id: request.Id);
        if (customer == null)
        {
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
