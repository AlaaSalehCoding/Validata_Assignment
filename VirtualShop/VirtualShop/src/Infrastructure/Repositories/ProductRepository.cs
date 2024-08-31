using VirtualShop.Application.Common.Interfaces;
using VirtualShop.Application.Products;
using VirtualShop.Domain.Entities;

namespace VirtualShop.Infrastructure.Repositories;

public class ProductRepository : CommoneRepository<Product>, IProductRepository
{
    public ProductRepository(IApplicationDbContext dbContext) : base(dbContext)
    {
        //TODO Add product specific code
    }

}
