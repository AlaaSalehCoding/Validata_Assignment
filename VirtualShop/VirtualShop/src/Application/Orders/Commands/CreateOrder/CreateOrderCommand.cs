using VirtualShop.Application.Common.Models;
using VirtualShop.Domain.Entities;

namespace CleanArchitecture.Application.Orders.Commands.CreateOrder;

public record CreateOrderCommand : IRequest<Result>
{
    public DateTime OrderDate { get; set; }
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<CreateOrderCommand, Order>();
        }
    }
}
