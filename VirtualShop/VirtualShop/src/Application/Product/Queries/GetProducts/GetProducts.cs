using VirtualShop.Application.Common.Interfaces;

namespace VirtualShop.Application.Product.Queries.GetProducts;

//add validation and authroization
public record GetProductsQuery : IRequest<ProductsVm>
{
}

public class GetProductsQueryValidator : AbstractValidator<GetProductsQuery>
{
    public GetProductsQueryValidator()
    {
    }
}

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, ProductsVm>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<ProductsVm> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var result =new ProductsVm(); 
        //TODO filtering and ordering
        var dbResult = _productRepository.Get();
        if (dbResult is not null)
        {
            result.Total = dbResult.Count();
            //TODO Apply pagination
            result.Lists = await dbResult.ProjectTo< ProductDto>(_mapper.ConfigurationProvider).ToListAsync();
        }
        return result;
    }
}
