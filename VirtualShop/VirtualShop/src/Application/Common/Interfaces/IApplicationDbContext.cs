

using VirtualShop.Domain.Common;

namespace VirtualShop.Application.Common.Interfaces;
public interface IApplicationDbContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    DbSet<TEntity> GetSet<TEntity>() where TEntity : BaseEntity;
}
