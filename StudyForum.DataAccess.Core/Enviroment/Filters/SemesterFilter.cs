using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyForum.DataAccess.Core.Enviroment.Filters
{
    public class SemesterFilter
    {
        public string NameQuery { get; set; }
        public DateTime? DateTimeBeginBefore { get; set; }
        public DateTime? DateTimeBeginAfter { get; set; }
        public DateTime? DateTimeEndBefore { get; set; }
        public DateTime? DateTimeEndAfter { get; set; }
    }
}
