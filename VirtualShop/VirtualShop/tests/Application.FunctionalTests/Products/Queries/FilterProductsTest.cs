using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualShop.Application.Common.Exceptions;
using VirtualShop.Application.Products.Commands.CreateProduct;
using VirtualShop.Application.Products.Queries.FilterProducts;
using VirtualShop.Domain.Entities;
using static VirtualShop.Application.FunctionalTests.Testing;

namespace VirtualShop.Application.FunctionalTests.Products.Queries;
public class FilterProductsTest : BaseTestFixture
{ 
    private async Task CreateTestBedAsync()
    {   
        await SendAsync(new CreateProductCommand
        {
            Name = "Test product 1",
            Price = 10
        });

        await SendAsync(new CreateProductCommand
        {
            Name = "Test product 2",
            Price = 20
        });


        await SendAsync(new CreateProductCommand
        {
            Name = "Test product 3",
            Price = 30
        });
    }

    [Test]
    public async Task ShouldFilterByPriceProduct()
    {
        var userId = await RunAsAdministratorAsync();
        await CreateTestBedAsync();

        var query = new FilterProductsQuery
        {
            Search = new Common.Filtration.SearchFilter { 
                FieldName = "Price", 
                FieldValue = "15", 
                Operator= Common.Filtration.Operator.GreaterThan, 
                LogicOperator=Common.Filtration.LogicOperator.And
            }
        };
        var result = await SendAsync(query);

        result.Items.Should().HaveCount(2);
    }


    [Test]
    public async Task ShouldFilterByNameProduct()
    {
        var userId = await RunAsAdministratorAsync();
        await CreateTestBedAsync();

        var query = new FilterProductsQuery
        {
            Search = new Common.Filtration.SearchFilter
            {
                FieldName = "Name",
                FieldValue = "2",
                Operator = Common.Filtration.Operator.Contains,
                LogicOperator = Common.Filtration.LogicOperator.And
            }
        };
        var result = await SendAsync(query);

        result.Items.Should().HaveCount(1);
    }
}
