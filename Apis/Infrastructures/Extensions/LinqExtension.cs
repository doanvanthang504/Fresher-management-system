using System;
using System.Linq;
using System.Linq.Expressions;

namespace Infrastructures.Extensions
{
    public static class LinqExtension
    {
        public static IQueryable<TEntity> WhereIfNotNull<TEntity>(
            this IQueryable<TEntity> query,
            Expression<Func<TEntity, bool>>? predicate)
        {
            if (query == null)
                throw new ArgumentNullException(nameof(query));
            if (predicate != null)
                query = query.Where(predicate);
            return query;
        }
    }
}
