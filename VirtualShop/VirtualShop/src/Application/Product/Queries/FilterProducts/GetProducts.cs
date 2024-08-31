using VirtualShop.Application.Common.Filtration;
using VirtualShop.Application.Common.Interfaces;

namespace VirtualShop.Application.Products.Queries.FilterProducts;

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
