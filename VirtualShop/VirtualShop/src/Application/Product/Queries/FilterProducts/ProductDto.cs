using System.ComponentModel.DataAnnotations;

namespace VirtualShop.Application.Product.Queries.FilterProducts;

public class ProductDto
{
    public ProductDto()
    { 
    }

    public long Id { get; init; } 
    public string Name { get; set; } = string.Empty; 
    public decimal Price { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Domain.Entities.Product, ProductDto>();
        }
    }
}
