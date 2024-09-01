using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Application.Orders.Commands.CreateOrder;
using VirtualShop.Application.Items.Commands.AddItemToOrder;
using VirtualShop.Application.Products.Commands.CreateProduct;
using VirtualShop.Domain.Entities;
using static VirtualShop.Application.FunctionalTests.Testing;

namespace VirtualShop.Application.FunctionalTests.Items.Commands;

public class AddItemToOrderTest : BaseTestFixture
{
    [Test]
    public async Task ShouldCreateOrder()
    {
        var userId = await RunAsAdministratorAsync();
        //Create product
        var productRes = await SendAsync(new CreateProductCommand
        {
            Name = "Test product 1",
            Price = 10
        });
        var product = await FindAsync<Product>(productRes.SuccessStatus!);
        product.Should().NotBeNull();
        product!.Price.Should().Be(10);

        var userId2 = await RunAsDefaultUserAsync();
        //Create Order 
        var orderRes = await SendAsync(new CreateOrderCommand
        {
            OrderDate = DateTime.Now,
        });
        //Add item to order
        var command = new AddItemToOrderCommand()
        {
            OrderId = (long)orderRes.SuccessStatus!,
            ProductId = (long)productRes.SuccessStatus!,
            Quantity = 2
        };
        var itemRes = await SendAsync(command);

        var item = await FindAsync<Item>(itemRes.SuccessStatus!);
        item.Should().NotBeNull();
        item!.OrderId.Should().Be(command.OrderId);
        item!.ProductId.Should().Be(command.ProductId);
        item!.Quantity.Should().Be(command.Quantity);
        item!.Price.Should().Be(command.Quantity * product!.Price);

        var order = await FindAsync<Order>(orderRes.SuccessStatus!);
        order.Should().NotBeNull();
        order!.TotalPrice.Should().Be(item.Price);
        order.LastModifiedBy.Should().Be(userId2);
        order.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}

