using VirtualShop.Application.Common.Models;
using VirtualShop.Application.Common.Security;
using VirtualShop.Domain.Constants;

namespace VirtualShop.Application.Products.Commands.DeleteProduct;

[Authorize(Roles = Roles.Administrator)]
[Authorize(Policy = Policies.CanManageProducts)]
public record DeleteProductCommand : IRequest<Result>
{
    public long Id { get; set; }
}
