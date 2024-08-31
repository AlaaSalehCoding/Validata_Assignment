using VirtualShop.Application.Common.Models;

namespace VirtualShop.Application.Orders.Queries.GetOrder;

public record GetOrderQuery : IRequest<Result>
{
    public long Id { get; set; }
}
