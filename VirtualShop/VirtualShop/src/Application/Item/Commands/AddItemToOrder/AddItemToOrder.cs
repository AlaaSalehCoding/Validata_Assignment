using VirtualShop.Application.Common.Interfaces;
using VirtualShop.Application.Common.Models;

namespace VirtualShop.Application.Item.Commands.AddItemToOrder;

public record AddItemToOrderCommand : IRequest<Result>
{
}

public class AddItemToOrderCommandValidator : AbstractValidator<AddItemToOrderCommand>
{
    public AddItemToOrderCommandValidator()
    {
    }
}

public class AddItemToOrderCommandHandler : IRequestHandler<AddItemToOrderCommand, Result>
{
    private readonly IApplicationDbContext _context;

    public AddItemToOrderCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public Task<Result> Handle(AddItemToOrderCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
