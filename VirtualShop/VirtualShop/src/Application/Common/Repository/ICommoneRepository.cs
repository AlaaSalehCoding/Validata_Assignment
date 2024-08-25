﻿using System.Collections.Generic;
using System.Linq.Expressions;
using VirtualShop.Domain.Common;

namespace VirtualShop.Application.Common.Repository;
public interface ICommoneRepository<TEntity, TId> where TEntity : BaseEntity<TId>
{
    void Add(TEntity entity);
    void Delete(TEntity entity);
    void Update(TEntity entity);
    IQueryable<TEntity> List(Expression<Func<TEntity, bool>> expression);
    TEntity? GetById(object id);
    IQueryable<TEntity> Get(
          Expression<Func<TEntity, bool>>? filter = null,
          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
          string includeProperties = "");
}
