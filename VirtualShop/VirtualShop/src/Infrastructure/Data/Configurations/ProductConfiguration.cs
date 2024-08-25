using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using VirtualShop.Domain.Entities;
using System.Reflection.Emit;

namespace VirtualShop.Infrastructure.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {  
        builder.Property(p => p.Price)
            .HasColumnType("decimal(18, 2)"); 
    }
}
