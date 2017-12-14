using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyForum.DataAccess.Core.Enviroment.Filters
{
    public class MessageFilter : ObjectFilterBase
    {
        public Guid? AuthorId { get; set; }
        public Guid? ThemeId { get; set; }
        public DateTime? CreateDateBefore { get; set; }
        public DateTime? CreateDateAfter { get; set; }
        public string SearchString { get; set; }
        public int? AttachedFilesCount { get; set; }
    }
}
