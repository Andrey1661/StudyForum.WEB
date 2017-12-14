using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicQueryFilter
{
    public static class QueriableExtensions
    {
        public static IQueryable<TTarget> Where<TFilter, TTarget>(
            this IQueryable<TTarget> query, 
            DynamicFilter<TFilter, TTarget> filter)
            where TFilter : class 
            where TTarget : class 
        {
            throw new NotImplementedException();
        }
    }
}
