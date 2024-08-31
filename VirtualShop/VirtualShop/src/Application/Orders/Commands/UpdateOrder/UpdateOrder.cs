using CleanArchitecture.Application.Orders.Commands.CreateOrder;
using VirtualShop.Application.Common.Interfaces;
using VirtualShop.Application.Common.Models;
using VirtualShop.Application.Common.Repository;
using VirtualShop.Domain.Entities;

namespace VirtualShop.Application.Orders.Commands.UpdateOrder;

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, Result>
{
    private readonly ICommonRepository<Order> _orderRrepo;
    private readonly IMapper _mapper;

    public UpdateOrderCommandHandler(ICommonRepository<Order> orderRrepo, IMapper mapper)
    {
        _orderRrepo = orderRrepo;
        _mapper = mapper;
    }

    public  async Task<Result> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var order =await  _orderRrepo.GetByIdAsync(request.Id);
        if (order == null)
        {
            return Result.Failure(["No such Order!."]);
        }
        _mapper.Map(request, order);
        await _orderRrepo.SaveChangesAsync();

        return Result.Success(order);
    }
}
