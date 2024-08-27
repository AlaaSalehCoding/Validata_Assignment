using VirtualShop.Application.Common.Filtration;
using VirtualShop.Application.Common.Interfaces;

namespace VirtualShop.Application.Product.Queries.FilterProducts;

//add validation and authroization
public record FilterProductsQuery : IRequest<FilterProductsResponce>, IFilter
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
        var dbResult = _productRepository.Get();
        if (dbResult is not null)
        {
            var query = dbResult.ProjectTo<ProductDto>(_mapper.ConfigurationProvider);

            query = query.ApplySearch(request.Search);
            query = query.ApplySorting(request.Sort);

            result.Total = await query.CountAsync();

            query = query.ApplyPagination(request.Pagination);

            result.Items = await query.ToListAsync();
        }
        return result;
    }
}
