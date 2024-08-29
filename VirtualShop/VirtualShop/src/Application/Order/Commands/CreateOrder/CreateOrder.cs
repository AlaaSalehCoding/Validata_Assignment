using VirtualShop.Application.Common.Interfaces;
using VirtualShop.Application.Common.Models;

namespace CleanArchitecture.Application.Order.Commands.CreateOrder;

public record CreateOrderCommand : IRequest<Result>
{
}

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
    }
}

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Result>
{
    private readonly IApplicationDbContext _context;

    public CreateOrderCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public  Task<Result> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
