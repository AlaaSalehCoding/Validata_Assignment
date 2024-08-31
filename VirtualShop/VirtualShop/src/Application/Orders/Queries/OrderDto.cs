using VirtualShop.Application.Product.Queries.FilterProducts;

namespace VirtualShop.Application.Orders.Queries;

public class OrderDto
{
    public OrderDto() { }
    public long Id { get; init; }
    public DateTime OrderDate { get; set; }
    public decimal TotalPrice { get; set; }
    public ICollection<ItemDto> Items { get; set; } = new List<ItemDto>();
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Domain.Entities.Order, OrderDto>();
        }
    }
}
