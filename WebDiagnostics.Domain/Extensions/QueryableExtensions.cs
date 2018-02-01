using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace WebDiagnostics.Domain.Extensions
{

public static class IQueryableExtensions 
{    
public static IOrderedQueryable<T> OrderByProperty<T>(this IQueryable<T> query, string propertyName, IComparer<object> comparer = null)
{
    return CallOrderedQueryable(query, "OrderBy", propertyName, comparer);
}

public static IOrderedQueryable<T> OrderByPropertyDescending<T>(this IQueryable<T> query, string propertyName, IComparer<object> comparer = null)
{
    return CallOrderedQueryable(query, "OrderByDescending", propertyName, comparer);
}

public static IOrderedQueryable<T> ThenBy<T>(this IOrderedQueryable<T> query, string propertyName, IComparer<object> comparer = null)
{
    return CallOrderedQueryable(query, "ThenBy", propertyName, comparer);
}

public static IOrderedQueryable<T> ThenByDescending<T>(this IOrderedQueryable<T> query, string propertyName, IComparer<object> comparer = null)
{
    return CallOrderedQueryable(query, "ThenByDescending", propertyName, comparer);
}

public static IOrderedQueryable<T> CallOrderedQueryable<T>(this IQueryable<T> query, string methodName, string propertyName,
IComparer<object> comparer = null)
{
    var param = Expression.Parameter(typeof(T), "x");

    var body = propertyName.Split('.').Aggregate<string, Expression>(param, Expression.PropertyOrField);

    return comparer != null
        ? (IOrderedQueryable<T>)query.Provider.CreateQuery(
            Expression.Call(
                typeof(Queryable),
                methodName,
                new[] { typeof(T), body.Type },
                query.Expression,
                Expression.Lambda(body, param),
                Expression.Constant(comparer)
            )
        )
        : (IOrderedQueryable<T>)query.Provider.CreateQuery(
            Expression.Call(
                typeof(Queryable),
                methodName,
                new[] { typeof(T), body.Type },
                query.Expression,
                Expression.Lambda(body, param)
            )
        );
        }
    }
 
}
