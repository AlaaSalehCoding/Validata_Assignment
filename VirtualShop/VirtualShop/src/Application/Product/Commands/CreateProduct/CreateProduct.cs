using System.ComponentModel.DataAnnotations;
using VirtualShop.Application.Common.Interfaces;
using VirtualShop.Application.Common.Models;
using VirtualShop.Application.Product.Queries.FilterProducts;

namespace VirtualShop.Application.Product.Commands.CreateProduct;

//Add validation & authorization
public record CreateProductCommand : IRequest<Result>
{ 
    public string Name { get; set; } = string.Empty; 
    public decimal Price { get; set; } 
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<CreateProductCommand, Domain.Entities.Product>();
        }
    }
}

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
    }
}

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

        return Result.Success(new { Id = product.Id });
    }
}
