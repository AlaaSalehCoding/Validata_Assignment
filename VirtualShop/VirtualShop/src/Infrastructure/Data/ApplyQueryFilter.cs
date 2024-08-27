using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;

namespace VirtualShop.Infrastructure.Data;
public static class ModelBuilderApplyQueryExtensions
{
    public static void ApplyQueryFilter<TBaseEntity>(this ModelBuilder builder,
        Expression<Func<TBaseEntity, bool>> filter)
    {
        var acceptableItems = builder.Model.GetEntityTypes()
            .Where(et => typeof(TBaseEntity).IsAssignableFrom(et.ClrType))
            .ToList();

        foreach (var entityType in acceptableItems)
        {
            var entityParam = Expression.Parameter(entityType.ClrType, "e");

            // replacing parameter with actual type
            var filterBody = ReplacingExpressionVisitor.Replace(filter.Parameters[0], entityParam, filter.Body);

            var filterLambda = entityType.GetQueryFilter();
            // Other filter already present, combine them
            if (filterLambda != null)
            {
                filterBody = ReplacingExpressionVisitor.Replace(entityParam, filterLambda.Parameters[0], filterBody);
                filterBody = Expression.AndAlso(filterLambda.Body, filterBody);
                filterLambda = Expression.Lambda(filterBody, filterLambda.Parameters);
            }
            else
            {
                filterLambda = Expression.Lambda(filterBody, entityParam);
            }

            entityType.SetQueryFilter(filterLambda);
        }
    }
}
