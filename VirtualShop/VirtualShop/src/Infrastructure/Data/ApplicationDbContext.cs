using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VirtualShop.Application.Common.Interfaces;
using VirtualShop.Domain.Common;
using VirtualShop.Infrastructure.Identity;

namespace VirtualShop.Infrastructure.Data;
public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
      
    public DbSet<TEntity> GetSet<TEntity>() where TEntity : BaseEntity
    {
        return this.Set<TEntity>();
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
