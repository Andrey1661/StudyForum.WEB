using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudyForum.WEB.ViewModels.Filters
{
    public class RepositoryFileFliter
    {
        public string SearchString { get; set; }
        public string Extension { get; set; }
        public DateTime? UploadDateAfter { get; set; }
        public DateTime? UploadDateBefore { get; set; }
    }
}