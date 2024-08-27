using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualShop.Application.Common.Repository;


namespace VirtualShop.Application.Product;

public interface IProductRepository : ICommonRepository<Domain.Entities.Product>
{
    Task DeleteAsync(long id);
}
