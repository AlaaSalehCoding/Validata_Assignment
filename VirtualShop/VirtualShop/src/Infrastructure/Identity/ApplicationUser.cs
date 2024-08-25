using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace VirtualShop.Infrastructure.Identity;
public class ApplicationUser : IdentityUser
{  
    public bool IsActive { get; set; } = true;
}

