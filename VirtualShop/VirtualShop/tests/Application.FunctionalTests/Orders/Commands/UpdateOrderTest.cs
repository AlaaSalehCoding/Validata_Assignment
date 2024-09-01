using CleanArchitecture.Application.Orders.Commands.CreateOrder;
using VirtualShop.Application.Orders.Commands.UpdateOrder;
using VirtualShop.Domain.Entities;
using static VirtualShop.Application.FunctionalTests.Testing;

namespace VirtualShop.Application.FunctionalTests.Orders.Commands;

public class UpdateOrderTest : BaseTestFixture
{
    [Test]
    public async Task ShouldUpdateOrder()
    {
        var userId = await RunAsDefaultUserAsync();

        CreateOrderCommand command = new CreateOrderCommand
        {
            OrderDate = DateTime.Now,
        };
        var orderRes = await SendAsync(command);
        orderRes.Succeeded.Should().BeTrue();

        await Task.Delay(100);
        var updateCommand = new UpdateOrderCommand() { Id = (long)orderRes.SuccessStatus!, OrderDate = DateTime.Now };
        var updateOrderRes = await SendAsync(updateCommand);
        updateOrderRes.Succeeded.Should().BeTrue();

        var order = await FindAsync<Order>(updateCommand.Id);
        order.Should().NotBeNull();
        order!.OrderDate.Should().Be(updateCommand.OrderDate);
    }

    [Test]
    public async Task ShouldNotUpdateOrder()
    {
        var userId = await RunAsDefaultUserAsync();
        var updateCommand = new UpdateOrderCommand() { Id = 100, OrderDate = DateTime.Now };
        var updateOrderRes = await SendAsync(updateCommand);
        updateOrderRes.Succeeded.Should().BeFalse();
    }
}
