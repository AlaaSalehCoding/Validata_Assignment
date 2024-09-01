using CleanArchitecture.Application.Orders.Commands.CreateOrder;
using VirtualShop.Application.Orders.Commands.DeleteOrder;
using VirtualShop.Domain.Entities;
using static VirtualShop.Application.FunctionalTests.Testing;

namespace VirtualShop.Application.FunctionalTests.Orders.Commands;

public class DeleteOrderTest : BaseTestFixture
{
    [Test]
    public async Task ShouldDeleteOrder()
    {
        var userId = await RunAsDefaultUserAsync();

        CreateOrderCommand command = new CreateOrderCommand
        {
            OrderDate = DateTime.Now,
        };
        var orderRes = await SendAsync(command);
        orderRes.Succeeded.Should().BeTrue();

        var deleteCommand= new DeleteOrderCommand() { Id = (long )orderRes.SuccessStatus!};
        var deleteOrderRes = await SendAsync(deleteCommand);
        deleteOrderRes.Succeeded.Should().BeTrue();

        var order = await FindAsync<Order>(deleteCommand.Id); 
        order.Should().BeNull(); 
    }

    [Test]
    public async Task ShouldNotDeleteOrder()
    {
        var userId = await RunAsDefaultUserAsync();

        var deleteCommand = new DeleteOrderCommand() { Id = 100 };
        var deleteOrderRes = await SendAsync(deleteCommand);
        deleteOrderRes.Succeeded.Should().BeFalse(); 
    }
}
