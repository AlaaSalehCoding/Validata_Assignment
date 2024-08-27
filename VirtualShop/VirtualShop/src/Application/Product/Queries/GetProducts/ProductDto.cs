using System.ComponentModel.DataAnnotations;

namespace VirtualShop.Application.Product.Queries.GetProducts;

public class ProductDto
{
    public ProductDto()
    { 
    }

    public int Id { get; init; } 
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
