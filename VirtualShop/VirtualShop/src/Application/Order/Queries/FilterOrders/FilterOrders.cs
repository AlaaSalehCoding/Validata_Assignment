using VirtualShop.Application.Common.Interfaces;
using VirtualShop.Application.Common.Models;

namespace VirtualShop.Application.Order.Queries.FilterOrders;

public record FilterOrdersQuery : IRequest<Result>
{
}

public class FilterOrdersQueryValidator : AbstractValidator<FilterOrdersQuery>
{
    public FilterOrdersQueryValidator()
    {
    }
}

public class FilterOrdersQueryHandler : IRequestHandler<FilterOrdersQuery, Result>
{
    private readonly IApplicationDbContext _context;

    public FilterOrdersQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public Task<Result> Handle(FilterOrdersQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
