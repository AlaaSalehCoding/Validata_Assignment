using VirtualShop.Application.Common.Models;
using VirtualShop.Application.Common.Security;
using VirtualShop.Domain.Constants;

namespace VirtualShop.Application.Products.Commands.CreateProduct;

[Authorize(Roles = Roles.Administrator)]
[Authorize(Policy = Policies.CanManageProducts)]
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
