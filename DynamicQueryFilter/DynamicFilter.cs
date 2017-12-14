using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DynamicQueryFilter.Attributes;

namespace DynamicQueryFilter
{
    public class DynamicFilter<TFilter, TTarget> 
        where TFilter : class 
        where TTarget : class
    {
        private IDictionary<CompareTypes, Func<Expression, Expression, Expression>> Expressions { get; }

        public DynamicFilter()
        {
            Expressions = new Dictionary<CompareTypes, Func<Expression, Expression, Expression>>
            {
                {CompareTypes.Equal, Expression.Equal},
                {CompareTypes.NotEqual, Expression.NotEqual},
                {CompareTypes.MoreThan, Expression.GreaterThan},
                {CompareTypes.MoreThanOrEqual, Expression.GreaterThanOrEqual},
                {CompareTypes.LessThan, Expression.LessThan},
                {CompareTypes.LessThanOrEqual, Expression.LessThanOrEqual}
            };
        }

        public Expression<Func<TTarget, bool>> CreateWhereExpression(TFilter filter)
        {
            var properties = typeof(TFilter).GetProperties().AsQueryable()
                .Where(p => p.IsDefined(typeof(FilterPropertyAttribute)) &&
                            p.GetValue(filter, null) != null);
                            
            var param = Expression.Parameter(typeof(TTarget), "t");
            Expression<Func<TTarget, bool>> result = null;

            foreach (var property in properties)
            {
                var attribute = property.GetCustomAttribute<FilterPropertyAttribute>();
                string propName = attribute.CompareProperty;
                CompareTypes compareType = attribute.CompareType;
                PropertyInfo targetProperty = typeof(TTarget).GetProperty(propName);

                if (targetProperty == null)
                {
                    throw new Exception();
                }

                Type type = targetProperty.PropertyType;
                object value = property.GetValue(filter, null);

                if (typeof(IComparable<>).MakeGenericType(value.GetType()).IsAssignableFrom(targetProperty.PropertyType))
                {
                    var prop = GetMemberExpression(param, propName);
                    var constant = Expression.Constant(value);
                    var body = Expressions[compareType].Invoke(prop, constant);
                    var expression = Expression.Lambda<Func<TTarget, bool>>(body, param);
                    result = result == null
                        ? expression
                        : result.AndAlso(expression);
                }
                else
                {
                    throw new Exception();
                }
            }

            return result;
        }

        private Expression GetMemberExpression(Expression parameter, string propName)
        {
            while (true)
            {
                if (!propName.Contains("."))
                {
                    return Expression.Property(parameter, propName);
                }

                int index = propName.IndexOf(".");
                var subParameter = Expression.Property(parameter, propName.Substring(0, index));
                parameter = subParameter;
                propName = propName.Substring(index + 1);
            }
        }
    }
}
