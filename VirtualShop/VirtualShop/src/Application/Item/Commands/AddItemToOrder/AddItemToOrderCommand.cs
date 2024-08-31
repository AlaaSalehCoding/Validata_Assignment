using VirtualShop.Application.Common.Models;

namespace VirtualShop.Application.Items.Commands.AddItemToOrder;

public record AddItemToOrderCommand : IRequest<Result>
{
    public int Quantity { get; set; }
    public long OrderId { get; set; }
    public long ProductId { get; set; }
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<AddItemToOrderCommand, Domain.Entities.Item>();
        }
    }
}
