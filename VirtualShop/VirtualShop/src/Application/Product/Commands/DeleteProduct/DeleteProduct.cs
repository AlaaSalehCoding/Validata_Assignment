using VirtualShop.Application.Common.Interfaces;
using VirtualShop.Application.Common.Models;

namespace VirtualShop.Application.Product.Commands.DeleteProduct;

//Add validation & authorization
public record DeleteProductCommand : IRequest<Result>
{
    public long Id { get; set; }
}

public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
    }
}

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Result>
{
    private readonly IProductRepository _productRepository;

    public DeleteProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Result> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetById(request.Id);
        if (product is null)
        {
            return Result.Failure(["no such product!"]);
        }
        await _productRepository.DeleteAsync(product);

        return Result.Success();
    }
}
