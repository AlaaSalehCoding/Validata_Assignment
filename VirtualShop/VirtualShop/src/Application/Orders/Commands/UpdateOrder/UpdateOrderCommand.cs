using VirtualShop.Application.Common.Models;
using VirtualShop.Domain.Entities;

namespace VirtualShop.Application.Orders.Commands.UpdateOrder;

public record UpdateOrderCommand : IRequest<Result>
{
    public long Id { get; set; }
    public DateTime? OrderDate { get; set; } 
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<UpdateOrderCommand, Order>();
        }
    }
}
