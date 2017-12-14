using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DynamicQueryFilter
{
    internal static class ExpressionExtensions
    {
        internal static Expression<Func<T, bool>> AndAlso<T>(this Expression<Func<T, bool>> exp1, Expression<Func<T, bool>> exp2)
        {
            var parameter = Expression.Parameter(typeof(T), "t");

            var leftVisitor = new ReplaceVisitor(exp1.Parameters[0], parameter);
            var left = leftVisitor.Visit(exp1.Body);

            var rightVisitor = new ReplaceVisitor(exp2.Parameters[0], parameter);
            var right = rightVisitor.Visit(exp2.Body);

            return Expression.Lambda<Func<T, bool>>(
                Expression.AndAlso(left, right), parameter);
        }

        internal static Expression<Func<T, bool>> OrElse<T>(this Expression<Func<T, bool>> exp1, Expression<Func<T, bool>> exp2)
        {
            var parameter = Expression.Parameter(typeof(T), "t");

            var leftVisitor = new ReplaceVisitor(exp1.Parameters[0], parameter);
            var left = leftVisitor.Visit(exp1.Body);

            var rightVisitor = new ReplaceVisitor(exp2.Parameters[0], parameter);
            var right = rightVisitor.Visit(exp2.Body);

            return Expression.Lambda<Func<T, bool>>(
                Expression.OrElse(left, right), parameter);
        }

        private class ReplaceVisitor : ExpressionVisitor
        {
            private readonly Expression _oldValue;
            private readonly Expression _newValue;

            public ReplaceVisitor(Expression oldValue, Expression newValue)
            {
                _oldValue = oldValue;
                _newValue = newValue;
            }

            public override Expression Visit(Expression node)
            {
                return node == _oldValue ? _newValue : base.Visit(node);
            }

        }
    }
}
