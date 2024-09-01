using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Application.Orders.Commands.CreateOrder;
using VirtualShop.Application.Common.Exceptions;
using VirtualShop.Application.Products.Commands.CreateProduct;
using VirtualShop.Domain.Entities;
using static VirtualShop.Application.FunctionalTests.Testing;

namespace VirtualShop.Application.FunctionalTests.Orders.Commands;
public class CreatOrderTest : BaseTestFixture
{  
    [Test]
    public async Task ShouldCreateOrder()
    {
        var userId = await RunAsDefaultUserAsync();

        CreateOrderCommand command = new CreateOrderCommand
        {
            OrderDate = DateTime.Now,
        };
        var orderRes = await SendAsync(command);
        orderRes.Succeeded.Should().BeTrue();
        orderRes.SuccessStatus.Should().NotBeNull();

        var order = await FindAsync<Order>(orderRes.SuccessStatus!);

        order.Should().NotBeNull();
        order!.OrderDate.Should().Be(command.OrderDate);
        order.CreatedBy.Should().Be(userId);
        order.Created.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
