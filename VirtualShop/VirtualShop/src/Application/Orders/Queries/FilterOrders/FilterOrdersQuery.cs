using VirtualShop.Application.Common.Filtration;

namespace VirtualShop.Application.Orders.Queries.FilterOrders;

public record FilterOrdersQuery : IRequest<FilterOrdersResponce>, IFilter
{
    public SearchFilter? Search { get; set; }
    public SortFilter? Sort { get; set; }
    public PaginationFilter? Pagination { get; set; }
} 
