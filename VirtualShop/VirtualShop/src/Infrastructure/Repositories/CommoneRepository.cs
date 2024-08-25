using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using VirtualShop.Application.Common.Interfaces;
using VirtualShop.Application.Common.Repository;
using VirtualShop.Domain.Common;

namespace VirtualShop.Infrastructure.Repositories;
public class CommoneRepository<TEntity, TId> : ICommoneRepository<TEntity, TId> where TEntity : BaseEntity<TId>
{
    private readonly IApplicationDbContext _dbContext;
    private DbSet<TEntity>? _dbSet = null;
    protected DbSet<TEntity> DbSet
    {
        get => _dbSet ??= _dbContext.GetSet<TEntity, TId>();
    }
    protected CommoneRepository(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public virtual void Add(TEntity entity)
    {
        DbSet.AddAsync(entity);
    }
    public virtual void Update(TEntity entity)
    {
        DbSet.Update(entity);
    }
    public virtual void Delete(TEntity entity)
    {
        if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)))
        {
            ((ISoftDelete)entity).IsDeleted = true;
            DbSet.Update(entity);
        }
        else
            DbSet.Remove(entity);
    }

    public virtual IQueryable<TEntity> List(Expression<Func<TEntity, bool>> expression)
    {
        return DbSet.Where(expression);
    }

    public virtual TEntity? GetById(object id)
    {
        return DbSet.Find(id);
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
}
