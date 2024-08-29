using VirtualShop.Application.Common.Interfaces;
using VirtualShop.Application.Common.Models;

namespace VirtualShop.Application.Order.Commands.DeleteOrder;

public record DeleteOrderCommand : IRequest<Result>
{
    public long Id { get; set; }
}

public class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
{
    public DeleteOrderCommandValidator()
    {
    }
}

public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, Result>
{
    private readonly IApplicationDbContext _context;

    public DeleteOrderCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public Task<Result> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
