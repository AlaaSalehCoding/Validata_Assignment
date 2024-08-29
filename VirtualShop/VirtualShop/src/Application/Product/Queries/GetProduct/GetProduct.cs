using VirtualShop.Application.Common.Interfaces;
using VirtualShop.Application.Common.Models;

namespace VirtualShop.Application.Product.Queries.GetProduct;

public record GetProductQuery : IRequest<Result>
{
}

public class GetProductQueryValidator : AbstractValidator<GetProductQuery>
{
    public GetProductQueryValidator()
    {
    }
}

public class GetProductQueryHandler : IRequestHandler<GetProductQuery, Result>
{
    private readonly IApplicationDbContext _context;

    public GetProductQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public Task<Result> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
