using System.Reflection;
using System.Runtime.CompilerServices;
using AutoMapper;
using NUnit.Framework; 
using static NUnit.Framework.Interfaces.TNode;
using VirtualShop.Application.Common.Interfaces;
using VirtualShop.Application.Items.Commands.AddItemToOrder;
using VirtualShop.Domain.Entities;
using CleanArchitecture.Application.Orders.Commands.CreateOrder;
using VirtualShop.Application.Orders.Commands.UpdateOrder;
using VirtualShop.Application.Orders.Queries;
using VirtualShop.Application.Products.Commands.CreateProduct;
using VirtualShop.Application.Products.Queries;

namespace Project1.Application.UnitTests.Common.Mappings;
public class MappingTests
{
    private readonly IConfigurationProvider _configuration;
    private readonly IMapper _mapper;

    public MappingTests()
    {
        _configuration = new MapperConfiguration(config =>
            config.AddMaps(Assembly.GetAssembly(typeof(IApplicationDbContext))));

        _mapper = _configuration.CreateMapper();
    }
    [Test]
    [TestCase(typeof(AddItemToOrderCommand), typeof(Item))]
    [TestCase(typeof(CreateOrderCommand), typeof(Order))]
    [TestCase(typeof(UpdateOrderCommand), typeof(Order))]
    [TestCase(typeof(Item), typeof(ItemDto))]
    [TestCase(typeof(Order), typeof(OrderDto))]
    [TestCase(typeof(CreateProductCommand), typeof(Product))]
    [TestCase(typeof(Product), typeof(ProductDto))] 
    public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
    {
        var instance = GetInstanceOf(source);

        _mapper.Map(instance, source, destination);
    }

    private object GetInstanceOf(Type type)
    {
        if (type.GetConstructor(Type.EmptyTypes) != null)
            return Activator.CreateInstance(type)!;

        // Type without parameterless constructor
        return RuntimeHelpers.GetUninitializedObject(type);
    }
}
