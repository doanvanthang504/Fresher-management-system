using Global.Shared.Commons;
using System;
using System.Linq;

namespace Infrastructures.Extensions
{
    public static class OrderByExtension
    {
        public static IOrderedQueryable<TEntity> OrderBy<TEntity>(
            this IQueryable<TEntity> query,
            SortingConditionQueue<TEntity>? sortingConditions)
        {
            if (query == null)
                throw new ArgumentNullException(nameof(query));
            if (sortingConditions == null || !sortingConditions.Any())
                return (IOrderedQueryable<TEntity>)query.Select(e => e);

            query = query.OrderBy(sortingConditions.First());
            foreach (var sortingCondition in sortingConditions.Skip(1))
                query = (query as IOrderedQueryable<TEntity>)!.ThenBy(sortingCondition);
            return (IOrderedQueryable<TEntity>)query;
        }

        public static IOrderedQueryable<TEntity> OrderBy<TEntity>(
            this IQueryable<TEntity> query,
            SortingCondition<TEntity> sortingCondition)
        {
            if (query == null)
                throw new ArgumentNullException(nameof(query));
            if (sortingCondition == null)
                throw new ArgumentNullException(nameof(sortingCondition));
            if (sortingCondition.Direction == SortingDirection.Ascending)
                return query.OrderBy(sortingCondition.SortExpression);
            return query.OrderByDescending(sortingCondition.SortExpression);
        }

        private static IOrderedQueryable<TEntity> ThenBy<TEntity>(
            this IOrderedQueryable<TEntity> query,
            SortingCondition<TEntity> sortingCondition)
        {
            if (sortingCondition.Direction == SortingDirection.Ascending)
                return query.ThenBy(sortingCondition.SortExpression);
            return query.ThenByDescending(sortingCondition.SortExpression);
        }
    }
}
