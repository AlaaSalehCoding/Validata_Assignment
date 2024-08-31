using VirtualShop.Application.Common.Interfaces;
using VirtualShop.Application.Common.Models;
using VirtualShop.Application.Common.Repository;
using VirtualShop.Application.Product.Commands.CreateProduct;
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

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
    }
}

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Result>
{
    private readonly ICommonRepository<Order> _orderRrepo;
    private readonly ICommonRepository<Customer> _customerRrepo;
    private readonly IMapper _mapper;
    private readonly IUser _user;

    public CreateOrderCommandHandler(
        ICommonRepository<Order> orderRrepo,
        ICommonRepository<Customer> customerRrepo,
        IMapper mapper,
        IUser user
        )
    {
        _orderRrepo = orderRrepo;
        _customerRrepo = customerRrepo;
        _mapper = mapper;
        _user = user;
    }

    public async Task<Result> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var LogedInCustomer = await _customerRrepo.Get(o => o.UserId == _user.Id).FirstOrDefaultAsync();
        if (LogedInCustomer == null)
        {
            return Result.Failure(["Current user can't create orders"]);
        }

        var newOrder = _mapper.Map<Order>(request);
        newOrder.CustomerId = LogedInCustomer.Id;

        await _orderRrepo.AddAsync(newOrder);
        if (newOrder.Id > 0)
        {
            return Result.Success(newOrder.Id);
        }
        return Result.Failure(["Order not created"]);
    }
}
