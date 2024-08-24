using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace VirtualShop.Infrastructure.Identity;
public class ApplicationUser : IdentityUser
{
    [Required ]
    [StringLength(50 )]
    public string FirstName { get; set; }=string.Empty;

    [Required]
    [StringLength(50)]
    public string LastName { get; set; } = string.Empty;
     
    [Required]
    [StringLength(250)]
    public string Address { get; set; } = string.Empty;

    [Required]
    [StringLength(5)]
    public string PostalCode { get; set; } = string.Empty;
}

