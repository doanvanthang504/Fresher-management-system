using System;
using System.Linq.Expressions;


namespace Global.Shared.Helpers
{
    public static class ExpressionHelper<T>
    {
        public static Expression<Func<T, bool>> ExpressionCombineAndAlso(
            Expression<Func<T, bool>> leftExpression,
            Expression<Func<T, bool>> rightExpression)
        {
            var param = Expression.Parameter(typeof(T), "x");
            var body = Expression.AndAlso(Expression.Invoke(leftExpression, param),
                                              Expression.Invoke(rightExpression, param));
            var expression = Expression.Lambda<Func<T, bool>>(body, param);
            return expression;
        }
    }
}
