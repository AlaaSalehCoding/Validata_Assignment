using System.ComponentModel.DataAnnotations.Schema;

namespace VirtualShop.Domain.Entities;

public class Order : BaseAuditableEntity<long>, ISoftDeletable
{
    public Order(long id) : base(id) { }
    public DateTime OrderDate { get; set; }
    public decimal TotalPrice { get; set; }

    public long CustomerId { get; set; }
    [ForeignKey(nameof(CustomerId))]
    public Customer? Customer { get; set; }

    public bool IsDeleted { get; set; } = false;
    public DateTime? DeletedOnDate { get; set; }
    public string? DeletedBy { get; set; }
    public ICollection<Item> Items { get; set; } = new List<Item>();
}
