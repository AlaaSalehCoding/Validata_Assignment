using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualShop.Application.Common.Models;

public class ApplicationUserCreateModel
{ 
    public required string Email { get; init; } 
    public required string Password { get; init; }

    [Required(ErrorMessage = "UserName is required")]
    [MinLength(4, ErrorMessage = "UserName must be at least 4 characters long")]
    public string UserName { get; set; } = string.Empty;

    [Required(ErrorMessage = "First name is required")]
    [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Last name is required")]
    [StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Address is required")]
    [StringLength(100, ErrorMessage = "Address cannot be longer than 100 characters")]
    public string Address { get; set; } = string.Empty;

    [Required(ErrorMessage = "Postal Code is required")]
    [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "Invalid Postal Code format")]
    public string PostalCode { get; set; } = string.Empty;
}
