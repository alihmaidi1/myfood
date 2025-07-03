
namespace Shared.Extensions;

public static class QueryableExtensions
{
    
    
    public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition,
        Expression<Func<T, bool>> predicate)
    {

        if (condition)
        {
            return query.Where(predicate);
        }

        return query;

    }
    
}