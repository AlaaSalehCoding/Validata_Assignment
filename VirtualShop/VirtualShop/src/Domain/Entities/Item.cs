using System.ComponentModel.DataAnnotations.Schema;

namespace VirtualShop.Domain.Entities;

public class Item : BaseAuditableEntity<long>, ISoftDelete
{ 
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public int ProductId { get; set; }

    public Product? Product { get; set; }
    [ForeignKey(nameof(ProductId))]

    public int OrderId { get; set; }
    [ForeignKey(nameof(OrderId))] 
    public Order? Order { get; set; }

    public bool IsDeleted { get; set; } = false;
}
