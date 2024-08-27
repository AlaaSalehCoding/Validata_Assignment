namespace VirtualShop.Application.Product.Queries.GetProducts;

public class ProductsVm
{
    public long Total { get; set; }

    public IReadOnlyCollection<ProductDto> Lists { get; set; } = Array.Empty<ProductDto>();
}
