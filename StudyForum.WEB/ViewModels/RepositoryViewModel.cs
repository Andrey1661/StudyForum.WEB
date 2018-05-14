using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudyForum.WEB.ViewModels
{
    public class RepositoryViewModel
    {
        public Guid Id { get; set; }
        public ICollection<ExtendedFileViewModel> Files { get; set; }
    }
}