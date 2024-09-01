using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Application.Orders.Commands.CreateOrder;
using NUnit.Framework.Constraints;
using VirtualShop.Application.Orders.Queries.FilterOrders;
using VirtualShop.Application.Products.Commands.CreateProduct;
using VirtualShop.Application.Products.Queries;
using VirtualShop.Application.Products.Queries.FilterProducts;
using VirtualShop.Application.Products.Queries.GetProduct;
using static VirtualShop.Application.FunctionalTests.Testing;

namespace VirtualShop.Application.FunctionalTests.Orders.Queries;
public class FilterOrderTest : BaseTestFixture
{
    private async Task CreateTestBedAsync()
    {
        await SendAsync(new CreateOrderCommand
        {
            OrderDate = DateTime.Now.AddDays(-1),
        });

        await SendAsync(new CreateOrderCommand
        {
            OrderDate = DateTime.Now.AddDays(1),
        });

        await SendAsync(new CreateOrderCommand
        {
            OrderDate = DateTime.Now.AddDays(2),
        });
    }

    [Test]
    public async Task ShouldFilterByDateOrders()
    {
        try
        {
            var userId = await RunAsDefaultUserAsync();
            await CreateTestBedAsync();

            await Task.Delay(1000);
            var query = new FilterOrdersQuery
            {
                Search = new Common.Filtration.SearchFilter
                {
                    FieldName = "OrderDate",
                    FieldValue =DateTime.Now,
                    Operator = Common.Filtration.Operator.GreaterThan,
                    LogicOperator = Common.Filtration.LogicOperator.And
                },
                Pagination = new Common.Filtration.PaginationFilter() { PageNumber = 1, PageSize = 10 },
                Sort = new Common.Filtration.SortFilter { FieldName = "OrderDate", SortDirection = Common.Filtration.SortDirection.asc }
            };
            var result = await SendAsync(query);

            result.Items.Should().HaveCount(2);
        }
        catch (Exception ex)
        {
            var x = ex.Message;
            throw;
        }
    }
}
