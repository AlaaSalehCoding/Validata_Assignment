﻿using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace VirtualShop.Domain.Entities;

public class Customer : BaseDeletableAuditableEntity<long>
{
    public Customer(long id = 0) : base(id) { }
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
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}
