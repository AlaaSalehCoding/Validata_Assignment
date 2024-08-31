using CleanArchitecture.Application.Orders.Commands.CreateOrder;
using VirtualShop.Application.Common.Interfaces;
using VirtualShop.Application.Common.Models;
using VirtualShop.Application.Common.Repository;
using VirtualShop.Domain.Entities;

namespace VirtualShop.Application.Orders.Commands.UpdateOrder;

public record UpdateOrderCommand : IRequest<Result>
{
    public int Id { get; set; }
    public DateTime? OrderDate { get; set; }
    public decimal? TotalPrice { get; set; }
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<UpdateOrderCommand, Order>();
        }
    }
}

public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(u=>u.TotalPrice).GreaterThanOrEqualTo(0);
    }
}

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
