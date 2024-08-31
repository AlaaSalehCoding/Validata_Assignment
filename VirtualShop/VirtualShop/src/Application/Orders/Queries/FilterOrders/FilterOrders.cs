using VirtualShop.Application.Common.Filtration;
using VirtualShop.Application.Common.Interfaces;
using VirtualShop.Application.Common.Models;
using VirtualShop.Application.Common.Repository;
using VirtualShop.Application.Products;
using VirtualShop.Application.Products.Queries.FilterProducts;
using VirtualShop.Domain.Entities;

namespace VirtualShop.Application.Orders.Queries.FilterOrders;

public record FilterOrdersQuery : IRequest<FilterOrdersResponce>, IFilter
{
    public SearchFilter? Search { get; set; }
    public SortFilter? Sort { get; set; }
    public PaginationFilter? Pagination { get; set; }
} 
public class FilterOrdersQueryValidator : AbstractValidator<FilterOrdersQuery>
{
    public FilterOrdersQueryValidator()
    {
    }
}

public class FilterOrdersResponce : FilteredResault<OrderDto>
{
}
public class FilterOrdersQueryHandler : IRequestHandler<FilterOrdersQuery, FilterOrdersResponce>
{
    private readonly ICommonRepository<Order> _orderRepo;
    private readonly IUser _user;
    private readonly IMapper _mapper;

    public FilterOrdersQueryHandler(ICommonRepository<Order> orderRepo, IUser user, IMapper mapper)
    {
        _orderRepo = orderRepo;
        _user = user;
        _mapper = mapper;
    }

    public async Task<FilterOrdersResponce> Handle(FilterOrdersQuery request, CancellationToken cancellationToken)
    {
        var result = new FilterOrdersResponce();
        var query = _orderRepo.Get(o => o.Customer != null && o.Customer.UserId == _user.Id,null, "Items.Product");
        if (query is not null)
        {

            query = query.ApplySearch(request.Search);

            result.Total = await query.CountAsync();

            result.Items = await query.ApplySorting(request.Sort)
                                    .ApplyPagination(request.Pagination)
                                    .ProjectTo<OrderDto>(_mapper.ConfigurationProvider)
                                    .ToListAsync();
        }
        return result;
    }
}
