using System.ComponentModel.DataAnnotations;

namespace VirtualShop.Domain.Entities;

public class Customer : BaseAuditableEntity<long>, ISoftDelete
{
    public string UserId { get; set; } = string.Empty;
    [Required]
    [StringLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [StringLength(250)]
    public string Address { get; set; } = string.Empty;

    [Required]
    [StringLength(5)]
    public string PostalCode { get; set; } = string.Empty;
    public bool IsDeleted { get; set; } = false;
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}
