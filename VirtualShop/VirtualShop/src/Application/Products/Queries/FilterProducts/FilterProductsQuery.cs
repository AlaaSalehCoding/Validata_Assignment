using VirtualShop.Application.Common.Filtration;

namespace VirtualShop.Application.Products.Queries.FilterProducts;

public record FilterProductsQuery : IRequest<FilterProductsResponce>    
{
    public SearchFilter? Search { get; set; }
    public SortFilter? Sort { get; set; }
    public PaginationFilter? Pagination { get; set; }
}
