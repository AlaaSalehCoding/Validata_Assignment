using VirtualShop.Application.Common.Exceptions;
using VirtualShop.Application.Products.Commands.CreateProduct;
using VirtualShop.Application.Products.Commands.DeleteProduct;
using VirtualShop.Application.Products.Commands.UpdateProduct;
using VirtualShop.Domain.Entities;
using static VirtualShop.Application.FunctionalTests.Testing;

namespace VirtualShop.Application.FunctionalTests.Products.Commands;

public class DeleteProductTests : BaseTestFixture
{
    [Test]
    public async Task ShouldDeleteProduct()
    {
        var userId = await RunAsAdministratorAsync();

        var command = new CreateProductCommand
        {
            Name = "Test product 1",
            Price = 10
        };
        var productRes = await SendAsync(command);
        productRes.Succeeded.Should().BeTrue();
        productRes.SuccessStatus.Should().NotBeNull();

        var product = await FindAsync<Product>(productRes.SuccessStatus!);
        product.Should().NotBeNull();

        var deleteCommand = new DeleteProductCommand() { Id = product!.Id };
        var result = await SendAsync(deleteCommand);
        result.Succeeded.Should().BeTrue(); 

        product = await FindAsync<Product>(productRes.SuccessStatus!);
        product.Should().BeNull();

    }

    [Test]
    public async Task ShouldNotDeleteProduct()
    {
        var userId = await RunAsDefaultUserAsync();

        var command = new DeleteProductCommand()
        {
            Id = 1, 
        };
        Assert.ThrowsAsync<ForbiddenAccessException>(async () => await SendAsync(command));
    }
}
