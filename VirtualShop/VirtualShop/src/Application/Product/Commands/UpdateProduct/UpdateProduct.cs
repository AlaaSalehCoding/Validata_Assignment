using VirtualShop.Application.Common.Interfaces;
using VirtualShop.Application.Common.Models;
using VirtualShop.Application.Product.Commands.CreateProduct;

namespace VirtualShop.Application.Product.Commands.UpdateProduct;
//Add validation & authorization
public record UpdateProductCommand : IRequest<Result>
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<UpdateProductCommand, Domain.Entities.Product>();
        }
    }
}

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
    }
}

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Result>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public UpdateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<Result> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetById(request.Id);
        if (product is null)
        {
            return Result.Failure(["no such product!"]);
        }
        _mapper.Map(product, request);
        await _productRepository.UpdateAsync(product);
        return Result.Success();
    }
}
