using VirtualShop.Application.Common.Interfaces;
using VirtualShop.Application.Common.Models;

namespace VirtualShop.Application.Order.Commands.UpdateOrder;

public record UpdateOrderCommand : IRequest<Result>
{
}

public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
    }
}

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, Result>
{
    private readonly IApplicationDbContext _context;

    public UpdateOrderCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public  Task<Result> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
