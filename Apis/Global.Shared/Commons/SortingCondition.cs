using System;
using System.Linq.Expressions;

namespace Global.Shared.Commons
{
    public class SortingCondition<TEntity>
    {
        public Expression<Func<TEntity, object>> SortExpression { get; }

        public SortingDirection Direction { get; set; }

        public SortingCondition(
            Expression<Func<TEntity, object>> sortExpression, 
            SortingDirection direction = SortingDirection.Ascending)
        {
            SortExpression = sortExpression ?? throw new ArgumentNullException(nameof(sortExpression));
            Direction = direction;
        }
    }
}
