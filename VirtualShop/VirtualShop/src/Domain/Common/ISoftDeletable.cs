using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace VirtualShop.Domain.Common;

public interface ISoftDeletable
{
    bool IsDeleted { get; set; }

    DateTime? DeletedOnDate { get; set; }
    string? DeletedBy { get; set; }
}
