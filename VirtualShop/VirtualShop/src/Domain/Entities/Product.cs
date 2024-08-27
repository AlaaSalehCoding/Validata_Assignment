using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualShop.Domain.Entities;
public class Product : BaseAuditableEntity<long>, ISoftDeletable
{
    public Product(long id) : base(id) { }

    [Required]
    [StringLength(250)]
    public string Name { get; set; } =string.Empty;
    
    [Required]
    public decimal Price { get; set; } 
    public bool IsDeleted { get; set; } = false;
    public DateTime? DeletedOnDate { get; set; }
    public string? DeletedBy { get; set; }
}
