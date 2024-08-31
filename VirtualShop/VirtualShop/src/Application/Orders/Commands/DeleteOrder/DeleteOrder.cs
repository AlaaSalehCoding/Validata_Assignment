using VirtualShop.Application.Common.Interfaces;
using VirtualShop.Application.Common.Models;
using VirtualShop.Application.Common.Repository;
using VirtualShop.Domain.Entities;

namespace VirtualShop.Application.Orders.Commands.DeleteOrder;

public record DeleteOrderCommand : IRequest<Result>
{
    public long Id { get; set; }
}

public class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
{
    public DeleteOrderCommandValidator()
    {
    }
}

public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, Result>
{
    private readonly ICommonRepository<Order> _orderRrepo;

    public DeleteOrderCommandHandler(ICommonRepository<Order> orderRrepo)
    {
        _orderRrepo = orderRrepo;
    }

    public async Task<Result> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRrepo.GetByIdAsync(request.Id);
        if (order == null)
        {
            return Result.Failure(["No such Order!."]);
        }
        await _orderRrepo.DeleteAsync(order);

        return Result.Success(order);
    }
}
