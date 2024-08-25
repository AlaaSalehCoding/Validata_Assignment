

using VirtualShop.Domain.Common;

namespace VirtualShop.Application.Common.Interfaces;
public interface IApplicationDbContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    DbSet<TEntity> GetSet<TEntity,TId>() where TEntity : BaseEntity<TId>;
}
