using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicQueryFilter
{
    public enum CompareTypes
    {
        Equal,
        NotEqual,
        MoreThan,
        MoreThanOrEqual,
        LessThan,
        LessThanOrEqual,
        Contains,
        Contained,
        StartsWith,
        EndsWith,
        Custom
    }
}
