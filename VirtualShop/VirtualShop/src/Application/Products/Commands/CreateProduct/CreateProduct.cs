using VirtualShop.Application.Common.Models;

namespace VirtualShop.Application.Products.Commands.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public CreateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<Result> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = _mapper.Map<Domain.Entities.Product>(request);
        if (product is null)
        {
            return Result.Failure(["Can't create product!"]);
        }
        await _productRepository.AddAsync(product);

        return Result.Success(product.Id );
    }
}
