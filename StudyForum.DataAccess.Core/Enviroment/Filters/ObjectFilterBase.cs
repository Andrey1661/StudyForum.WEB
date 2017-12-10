using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyForum.DataAccess.Core.Enviroment.Filters
{
    public class ObjectFilterBase
    {
        public ObjectFilterBase()
        {
            OrderBy = new List<string>();
        }

        public ObjectFilterBase(params string[] orderBy)
        {
            OrderBy = orderBy;
        }

        public IEnumerable<string> OrderBy { get; set; }
    }
}
