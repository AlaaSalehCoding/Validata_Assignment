using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualShop.Application.Common.Exceptions;
using VirtualShop.Application.Products.Commands.CreateProduct;
using VirtualShop.Domain.Entities;
using static VirtualShop.Application.FunctionalTests.Testing;

namespace VirtualShop.Application.FunctionalTests.Products.Commands;
public class CreateProductTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new CreateProductCommand();

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<Exception>();
    }

    [Test]
    public async Task ShouldCreateProduct()
    {
        var userId = await RunAsAdministratorAsync();

        CreateProductCommand command = new CreateProductCommand
        {
            Name = "Test product 1",
            Price = 10
        };
        var productRes = await SendAsync(command); 
        productRes.Succeeded.Should().BeTrue();
        productRes.SuccessStatus.Should().NotBeNull();

        var product = await FindAsync<Product>(productRes.SuccessStatus!);

        product.Should().NotBeNull(); 
        product!.Name.Should().Be(command.Name);
        product!.Price.Should().Be(command.Price);
        product.CreatedBy.Should().Be(userId);
        product.Created.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000)); 
    }

    [Test]
    public async Task ShouldNotCreateProduct()
    {
        var userId = await RunAsDefaultUserAsync();

        CreateProductCommand command = new CreateProductCommand
        {
            Name = "Test product 1",
            Price = 10
        };
        Assert.ThrowsAsync<ForbiddenAccessException>(async () => await SendAsync(command));
    }
}
