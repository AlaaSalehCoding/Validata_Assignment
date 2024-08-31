using VirtualShop.Application.Common.Interfaces;
using VirtualShop.Application.Common.Models;

namespace VirtualShop.Application.Product.Queries.GetProduct;

public record GetProductQuery : IRequest<Result>
{
    public long Id { get; set; }
}

public class GetProductQueryValidator : AbstractValidator<GetProductQuery>
{
    public GetProductQueryValidator()
    {
    }
}

public class GetProductQueryHandler : IRequestHandler<GetProductQuery, Result>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetProductQueryHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<Result> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id);
        if (product == null)
        {
            return Result.Failure(["No such product!."]);
        }
        var productDto = _mapper.Map<ProductDto>(product);
        return Result.Success(productDto);
    }
}
