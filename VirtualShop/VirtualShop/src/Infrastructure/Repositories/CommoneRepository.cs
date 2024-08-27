using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using VirtualShop.Application.Common.Interfaces;
using VirtualShop.Application.Common.Repository;
using VirtualShop.Domain.Common;

namespace VirtualShop.Infrastructure.Repositories;
public class CommoneRepository<TEntity > : ICommonRepository<TEntity > where TEntity : BaseEntity 
{
    private readonly IApplicationDbContext _dbContext;
    private DbSet<TEntity>? _dbSet = null;
    protected DbSet<TEntity> DbSet
    {
        get => _dbSet ??= _dbContext.GetSet<TEntity>();
    }
    public CommoneRepository(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public virtual async Task AddAsync(TEntity entity)
    {
        await DbSet.AddAsync(entity);
        await _dbContext.SaveChangesAsync(default);
    }
    public virtual async Task UpdateAsync(TEntity entity)
    {
        DbSet.Update(entity);
        await _dbContext.SaveChangesAsync(default);
    }
    public virtual async Task DeleteAsync(TEntity entity)
    {
        DbSet.Remove(entity);
        await _dbContext.SaveChangesAsync(default);
    }

    public virtual IQueryable<TEntity> List(Expression<Func<TEntity, bool>> expression)
    {
        return DbSet.Where(expression);
    }

    public virtual async Task<TEntity?> GetById(object id)
    {
        return await DbSet.FindAsync(id);
    }
    public virtual IQueryable<TEntity> Get(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeProperties = "")
    {
        IQueryable<TEntity> query = DbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        foreach (var includeProperty in includeProperties.Split
            (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }

        if (orderBy != null)
        {
            return orderBy(query);
        }
        return query;
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync(default);
    }
}
