using VirtualShop.Application.Common.Models;
using VirtualShop.Application.Common.Security;
using VirtualShop.Domain.Constants;

namespace VirtualShop.Application.Products.Commands.UpdateProduct;

[Authorize(Roles = Roles.Administrator)]
[Authorize(Policy = Policies.CanManageProducts)]
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
