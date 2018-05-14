using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyForum.DataAccess.Core.Enviroment.Filters
{
    public class RepositoryFileFilter
    {
        public string SearchString { get; set; }
        public string Extension { get; set; }
        public DateTime? UploadDateBefore { get; set; }
        public DateTime? UploadDateAfter { get; set; }
    }
}
