using VirtualShop.Application.Common.Filtration;
using VirtualShop.Application.Common.Interfaces;

namespace VirtualShop.Application.Product.Queries.FilterProducts;

public record FilterProductsQuery : IRequest<FilterProductsResponce>    
{
    public SearchFilter? Search { get; set; }
    public SortFilter? Sort { get; set; }
    public PaginationFilter? Pagination { get; set; }
}
public class FilterProductsResponce : FilteredResault<ProductDto>
{
}
public class FilterProductsQueryValidator : AbstractValidator<FilterProductsQuery>
{
    public FilterProductsQueryValidator()
    {
        //Products can be public
    }
}

public class FilterProductsQueryHandler : IRequestHandler<FilterProductsQuery, FilterProductsResponce>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public FilterProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<FilterProductsResponce> Handle(FilterProductsQuery request, CancellationToken cancellationToken)
    {
        var result = new FilterProductsResponce();
        var query = _productRepository.DbSet.AsQueryable();
        if (query is not null)
        {

            query = query.ApplySearch(request.Search);

            result.Total = await query.CountAsync(); 

            result.Items = await query.ApplySorting(request.Sort)
                                    .ApplyPagination(request.Pagination)
                                    .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
                                    .ToListAsync();
        }
        return result;
    }
}
