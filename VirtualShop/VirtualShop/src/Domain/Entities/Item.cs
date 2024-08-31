using System.ComponentModel.DataAnnotations.Schema;

namespace VirtualShop.Domain.Entities;

public class Item : BaseDeletableAuditableEntity<long>
{
    public Item(long id = 0) : base(id) { }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public long OrderId { get; set; }
    public long ProductId { get; set; }
    
    
    [ForeignKey(nameof(ProductId))]
    public Product? Product { get; set; }
    [ForeignKey(nameof(OrderId))]
    public Order? Order { get; set; }
}
