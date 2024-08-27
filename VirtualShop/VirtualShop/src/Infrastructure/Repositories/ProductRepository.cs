using VirtualShop.Application.Common.Interfaces;
using VirtualShop.Application.Product;
using VirtualShop.Domain.Entities;

namespace VirtualShop.Infrastructure.Repositories;

public class ProductRepository : CommoneRepository<Product>, IProductRepository
{
    public ProductRepository(IApplicationDbContext dbContext) : base(dbContext)
    {
    }
     
}
