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
    public IProductRepository ProductRepository { get; }
    public DeleteProductCommandHandler(IProductRepository productRepository)
    {
        ProductRepository = productRepository;
    }

    public async Task<Result> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        await ProductRepository.DeleteAsync(request.Id);

        return Result.Success();
    }
}
