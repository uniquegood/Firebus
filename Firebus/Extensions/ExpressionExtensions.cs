using System;
using System.Linq.Expressions;

namespace Firebus.Extensions
{
    public static class ExpressionExtensions
    {
        public static object Evaluate(this Expression expr)
        {
            var lambdaExpr = Expression.Lambda<Func<object, object>>(
                Expression.Convert(expr, typeof(object)),
                Expression.Parameter(typeof(object), "_"));

            var func = ExpressionUtil.CachedExpressionCompiler.Process(lambdaExpr);

            return func(null);
        }
    }
}
