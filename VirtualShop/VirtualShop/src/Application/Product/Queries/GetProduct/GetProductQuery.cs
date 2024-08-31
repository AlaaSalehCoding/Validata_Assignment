using VirtualShop.Application.Common.Models;

namespace VirtualShop.Application.Products.Queries.GetProduct;

public record GetProductQuery : IRequest<Result>
{
    public long Id { get; set; }
}
