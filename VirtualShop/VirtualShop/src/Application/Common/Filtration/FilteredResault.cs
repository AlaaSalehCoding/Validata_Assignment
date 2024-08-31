using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualShop.Application.Products.Queries.FilterProducts;

namespace VirtualShop.Application.Common.Filtration;
public abstract class FilteredResault<TItem>
{ 
    public virtual long Total { get; set; }
    public IReadOnlyCollection<TItem> Items { get; set; } = Array.Empty<TItem>();
}
