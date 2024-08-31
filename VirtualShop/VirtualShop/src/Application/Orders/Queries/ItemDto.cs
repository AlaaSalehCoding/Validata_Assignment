namespace VirtualShop.Application.Orders.Queries;

public class ItemDto
{
    public ItemDto() { }
    public long Id { get; init; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public long ProductId { get; set; }
    public string ProductName { get; set; } = "";
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Domain.Entities.Item, ItemDto>()
                .ForMember(d => d.ProductName, opt => opt.MapFrom(s => s.Product != null ? s.Product.Name : ""));
        }
    }
}
