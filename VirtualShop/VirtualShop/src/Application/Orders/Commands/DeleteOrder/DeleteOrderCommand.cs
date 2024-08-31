using VirtualShop.Application.Common.Models;

namespace VirtualShop.Application.Orders.Commands.DeleteOrder;

public record DeleteOrderCommand : IRequest<Result>
{
    public long Id { get; set; }
}
