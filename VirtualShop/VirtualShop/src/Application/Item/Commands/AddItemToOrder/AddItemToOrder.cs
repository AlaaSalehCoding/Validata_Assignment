﻿using VirtualShop.Application.Common.Interfaces;
using VirtualShop.Application.Common.Models;
using VirtualShop.Application.Common.Repository;
using VirtualShop.Application.Orders.Queries;
using VirtualShop.Domain.Entities;

namespace VirtualShop.Application.Items.Commands.AddItemToOrder;

public record AddItemToOrderCommand : IRequest<Result>
{
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public long OrderId { get; set; }
    public long ProductId { get; set; }
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<AddItemToOrderCommand,Domain.Entities.Item>();
        }
    }
}

public class AddItemToOrderCommandValidator : AbstractValidator<AddItemToOrderCommand>
{
    public AddItemToOrderCommandValidator()
    {
        RuleFor(i => i.Price).GreaterThan(0);
    }
}

public class AddItemToOrderCommandHandler : IRequestHandler<AddItemToOrderCommand, Result>
{
    private readonly ICommonRepository<Item> _itemRepo;
    private readonly ICommonRepository<Order> _orderRepo;
    private readonly IMapper _mapper;

    public AddItemToOrderCommandHandler(ICommonRepository<Item> itemRepo, ICommonRepository<Order> orderRepo, IMapper mapper)
    {
        _itemRepo = itemRepo;
        _orderRepo = orderRepo;
        _mapper = mapper;
    }

    public async Task<Result> Handle(AddItemToOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepo.GetByIdAsync(request.OrderId);
        if(order is null)
        {
            return Result.Failure(["No such order!."]);
        }

        var item = _mapper.Map<Item>(request);
        await _itemRepo.AddAsync(item);
        if (item.Id > 0)
        {
            order.TotalPrice += item.Price;
            await _orderRepo.UpdateAsync(order);
        }

        return Result.Success(item.Id);
    }
}
