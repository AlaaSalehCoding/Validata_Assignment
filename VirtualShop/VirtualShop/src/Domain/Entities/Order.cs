using System.ComponentModel.DataAnnotations.Schema;

namespace VirtualShop.Domain.Entities;

public class Order : BaseDeletableAuditableEntity<long>
{
    public Order(long id = 0) : base(id) { }

    public DateTime OrderDate { get; set; }
    public decimal TotalPrice { get; set; } 
    public long CustomerId { get; set; }

    [ForeignKey(nameof(CustomerId))]
    public Customer? Customer { get; set; }
    public ICollection<Item> Items { get; set; } = new List<Item>();
}
