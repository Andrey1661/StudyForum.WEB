using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyForum.DataAccess.Core.Enviroment.Filters
{
    public class FileFilter
    {
        public string SearchString { get; set; }
        public string Extension { get; set; }
        public Guid? MessageId { get; set; }
        public Guid? ThemeId { get; set; }
    }
}
