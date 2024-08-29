using VirtualShop.Application.Common.Interfaces;
using VirtualShop.Application.Common.Models;

namespace VirtualShop.Application.Order.Queries.GetOrder;

public record GetOrderQuery : IRequest<Result>
{
}

public class GetOrderQueryValidator : AbstractValidator<GetOrderQuery>
{
    public GetOrderQueryValidator()
    {
    }
}

public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, Result>
{
    private readonly IApplicationDbContext _context;

    public GetOrderQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public  Task<Result> Handle(GetOrderQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
