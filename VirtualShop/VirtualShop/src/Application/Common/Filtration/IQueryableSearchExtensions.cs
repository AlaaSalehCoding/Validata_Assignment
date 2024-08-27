using System.Linq.Expressions;

namespace VirtualShop.Application.Common.Filtration;

public static class IQueryableSearchExtensions
{
    public static Expression<Func<T, bool>> BuildSearchExpression<T>(SearchFilter filter, ParameterExpression parameter)
    {
        var property = Expression.Property(parameter, filter.FieldName);
        var constant = Expression.Constant(Convert.ChangeType(filter.FieldValue, property.Type));

        Expression comparison;

        switch (filter.Operator)
        {
            case Operator.Equals:
                comparison = Expression.Equal(property, constant);
                break;
            case Operator.Contains:
                comparison = Expression.Call(property, "Contains", null, constant);
                break;
            case Operator.GreaterThan:
                comparison = Expression.GreaterThan(property, constant);
                break;
            case Operator.LessThan:
                comparison = Expression.LessThan(property, constant);
                break;
            case Operator.GreaterThanOrEqual:
                comparison = Expression.GreaterThanOrEqual(property, constant);
                break;
            case Operator.LessThanOrEqual:
                comparison = Expression.LessThanOrEqual(property, constant);
                break;
            default:
                throw new NotSupportedException($"Operator {filter.Operator} is not supported");
        }


        if (filter.SubFilters != null && filter.SubFilters.Count > 0)
        {
            Expression? subExpression = null;

            foreach (var subFilter in filter.SubFilters)
            {
                var currentSubExpression = BuildSearchExpression<T>(subFilter, parameter).Body;

                if (subExpression == null)
                {
                    subExpression = currentSubExpression;
                }
                else
                {
                    subExpression = filter.LogicOperator == LogicOperator.Or
                        ? Expression.OrElse(subExpression, currentSubExpression)
                        : Expression.AndAlso(subExpression, currentSubExpression);
                }
            }
            if (subExpression != null)
            {
                comparison = Expression.AndAlso(comparison, subExpression);
            }
        }

        return Expression.Lambda<Func<T, bool>>(comparison, parameter);
    }

    public static IQueryable<T> ApplySearch<T>(this IQueryable<T> query, List<SearchFilter> searchFilters)
    {
        if (searchFilters == null || searchFilters.Count == 0)
        {
            return query;
        }

        var parameter = Expression.Parameter(typeof(T), "x");
        Expression? combinedExpression = null;

        foreach (var filter in searchFilters)
        {
            var searchExpression = BuildSearchExpression<T>(filter, parameter).Body;

            if (combinedExpression == null)
            {
                combinedExpression = searchExpression;
            }
            else
            {
                combinedExpression = filter.LogicOperator == LogicOperator.Or
                    ? Expression.OrElse(combinedExpression, searchExpression)
                    : Expression.AndAlso(combinedExpression, searchExpression);
            }
        }

        var lambda = Expression.Lambda<Func<T, bool>>(combinedExpression!, parameter);
        return query.Where(lambda);
    }
    public static IQueryable<T> ApplySearch<T>(this IQueryable<T> query, SearchFilter? searchFilter)
    {
        if (searchFilter is null )
        {
            return query;
        }

        var parameter = Expression.Parameter(typeof(T), "x"); 
        var searchExpression = BuildSearchExpression<T>(searchFilter, parameter).Body; 

        var lambda = Expression.Lambda<Func<T, bool>>(searchExpression, parameter);
        return query.Where(lambda);
    }
}
