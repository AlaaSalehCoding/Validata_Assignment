using CleanArchitecture.Application.Orders.Commands.CreateOrder;
using VirtualShop.Application.Orders.Queries;
using VirtualShop.Application.Orders.Queries.GetOrder;
using static VirtualShop.Application.FunctionalTests.Testing;

namespace VirtualShop.Application.FunctionalTests.Orders.Queries;

public class GetOrdersTest : BaseTestFixture
{

    [Test]
    public async Task ShouldGetOrder()
    {
        var userId = await RunAsDefaultUserAsync();
        var createCommand = new CreateOrderCommand
        {
            OrderDate = DateTime.Now.AddDays(-1),
        };
        var createRes = await SendAsync(createCommand);
        createRes.Succeeded.Should().BeTrue();

        var getQuery = new GetOrderQuery
        {
            Id = (long)createRes.SuccessStatus!
        };
        var result = await SendAsync(getQuery);
        result.Should().NotBeNull();
        result.SuccessStatus.Should().BeOfType<OrderDto>();

        var getProduct = result.SuccessStatus as OrderDto;
        getProduct.Should().NotBeNull();
        getProduct!.Id.Should().Be(getQuery.Id);
        getProduct!.OrderDate.Should().Be(createCommand.OrderDate);
    }

    [Test]
    public async Task ShouldNotGetOrder()
    {
        var userId = await RunAsDefaultUserAsync();
        var getQuery = new GetOrderQuery
        {
            Id = 100
        };
        var result = await SendAsync(getQuery);
        result.Should().NotBeNull();
        result.Succeeded.Should().BeFalse();
        result.SuccessStatus.Should().BeNull();
    }
}
