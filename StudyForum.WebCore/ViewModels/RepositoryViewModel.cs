using System;
using System.Collections.Generic;

namespace StudyForum.WebCore.ViewModels
{
    public class RepositoryViewModel
    {
        public Guid Id { get; set; }
        public ICollection<ExtendedFileViewModel> Files { get; set; }
    }
}