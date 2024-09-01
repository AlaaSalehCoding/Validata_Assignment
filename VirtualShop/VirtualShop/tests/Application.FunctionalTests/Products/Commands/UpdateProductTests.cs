using VirtualShop.Application.Common.Exceptions;
using VirtualShop.Application.Products.Commands.CreateProduct;
using VirtualShop.Application.Products.Commands.UpdateProduct;
using VirtualShop.Domain.Entities;
using static VirtualShop.Application.FunctionalTests.Testing;

namespace VirtualShop.Application.FunctionalTests.Products.Commands;

public class UpdateProductTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new UpdateProductCommand();

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<Exception>();
    }

    [Test]
    public async Task ShouldUpdateProduct()
    {
        var userId = await RunAsAdministratorAsync();

        var createCommand = new CreateProductCommand
        {
            Name = "Test product 1",
            Price = 10
        };
        var productRes = await SendAsync(createCommand);
        productRes.Succeeded.Should().BeTrue();
        productRes.SuccessStatus.Should().NotBeNull();
        var product = await FindAsync<Product>(productRes.SuccessStatus!);
        product.Should().NotBeNull();

        var updateCommand = new UpdateProductCommand()
        {
            Id = product!.Id,
            Name = product.Name + "Updates",
            Price = product.Price + 10,
        };
        var updateProductRes = await SendAsync(updateCommand);
        productRes.Succeeded.Should().BeTrue();
        product = await FindAsync<Product>(product.Id);
        product!.Name.Should().Be(createCommand.Name + "Updates");
        product!.Price.Should().Be(createCommand.Price + 10);
        product.LastModifiedBy.Should().Be(userId);
        product.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    } 

    [Test]
    public async Task ShouldNotUpdateProduct()
    {
        var userId = await RunAsDefaultUserAsync(); 
        var command = new UpdateProductCommand()
        {
            Id=1,
            Name = "Test product 1",
            Price = 10
        };
        Assert.ThrowsAsync<ForbiddenAccessException>(async () => await SendAsync(command));
    }
}
