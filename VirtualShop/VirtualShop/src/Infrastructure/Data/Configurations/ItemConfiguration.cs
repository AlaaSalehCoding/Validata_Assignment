using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using VirtualShop.Domain.Entities;
using System.Reflection.Emit;

namespace VirtualShop.Infrastructure.Data.Configurations;

public class ItemConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.Property(i => i.Price)
        .HasColumnType("decimal(18, 2)");
    }
}
