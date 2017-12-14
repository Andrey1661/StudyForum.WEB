using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicQueryFilter.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class FilterPropertyAttribute : Attribute
    {
        public FilterPropertyAttribute(CompareTypes compareType, string compareProperty)
        {
            CompareType = compareType;
            CompareProperty = compareProperty;
        }

        public CompareTypes CompareType { get; set; }
        public string CompareProperty { get; set; }
    }
}
