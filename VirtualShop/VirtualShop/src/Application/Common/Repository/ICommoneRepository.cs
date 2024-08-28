using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using VirtualShop.Domain.Common;

namespace VirtualShop.Application.Common.Repository;
public interface ICommonRepository<TEntity> where TEntity : BaseEntity
{
    public DbSet<TEntity> DbSet { get; }
    Task AddAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    IQueryable<TEntity> List(Expression<Func<TEntity, bool>> expression);
    Task<TEntity?> GetByIdAsync(object id);
    IQueryable<TEntity> Get(
          Expression<Func<TEntity, bool>>? filter = null,
          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
          string includeProperties = "");
    Task SaveChangesAsync();
}
