using VirtualShop.Application.Common.Interfaces;
using VirtualShop.Application.Common.Models;
using VirtualShop.Application.Common.Repository;
using VirtualShop.Domain.Entities;

namespace VirtualShop.Application.Orders.Queries.GetOrder;

public record GetOrderQuery : IRequest<Result>
{
    public long Id { get; set; }
}

public class GetOrderQueryValidator : AbstractValidator<GetOrderQuery>
{
    public GetOrderQueryValidator()
    {
    }
}

public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, Result>
{
    private readonly ICommonRepository<Order> _orderRepo;
    private readonly IUser _user;
    private readonly IMapper _mapper;

    public GetOrderQueryHandler(ICommonRepository<Order> orderRepo, IUser user, IMapper mapper)
    {
        _orderRepo = orderRepo;
        _user = user;
        _mapper = mapper;
    }

    public async Task<Result> Handle(GetOrderQuery request, CancellationToken cancellationToken)
    {
        var query = _orderRepo.Get(o => o.Id==request.Id && o.Customer != null && o.Customer.UserId == _user.Id, null, "Items.Product");
        if (query == null)
        {
            return Result.Failure(["Server error!."]);
        }
        var order = await query.FirstOrDefaultAsync(cancellationToken);
        if (order == null)
        {
            return Result.Failure(["No such order!."]);
        }
        var orderDto = _mapper.Map<OrderDto>(order);
        return Result.Success(orderDto);
    }
}
