using VirtualShop.Application.Products.Commands.CreateProduct;
using VirtualShop.Application.Products.Queries;
using VirtualShop.Application.Products.Queries.FilterProducts;
using VirtualShop.Application.Products.Queries.GetProduct;
using static VirtualShop.Application.FunctionalTests.Testing;

namespace VirtualShop.Application.FunctionalTests.Products.Queries;

public class GetProductsTest : BaseTestFixture
{

    [Test]
    public async Task ShouldGetProduct()
    {
        var userId = await RunAsAdministratorAsync();
        var createCommand = new CreateProductCommand
        {
            Name = "Test product 1",
            Price = 10
        };
        var createRes = await SendAsync(createCommand);
        createRes.Succeeded.Should().BeTrue();

        var getQuery = new GetProductQuery
        {
            Id = (long)createRes.SuccessStatus!
        };
        var result = await SendAsync(getQuery);
        result.Should().NotBeNull();
        result.SuccessStatus.Should().BeOfType<ProductDto>();

        var getProduct = result.SuccessStatus as ProductDto;
        getProduct.Should().NotBeNull();
        getProduct!.Id.Should().Be(getQuery.Id);
        getProduct!.Price.Should().Be(createCommand.Price);
        getProduct!.Name.Should().Be(createCommand.Name);
    }

    [Test]
    public async Task ShouldNotGetProduct()
    {
        var getQuery = new GetProductQuery
        {
            Id = 10
        };
        var result = await SendAsync(getQuery);
        result.Should().NotBeNull();
        result.Succeeded.Should().BeFalse();
        result.SuccessStatus.Should().BeNull(); 
    }
}
