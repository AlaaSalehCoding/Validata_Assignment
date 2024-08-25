using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using VirtualShop.Domain.Entities;
using System.Reflection.Emit;

namespace VirtualShop.Infrastructure.Data.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.Property(o => o.TotalPrice)
        .HasColumnType("decimal(18, 2)");
    }
}
