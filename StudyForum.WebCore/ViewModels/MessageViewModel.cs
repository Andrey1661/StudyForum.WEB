using System;
using System.Collections.Generic;

namespace StudyForum.WebCore.ViewModels
{
    public class MessageViewModel
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
        public string AuthorName { get; set; }
        public ICollection<DownloadFileViewModel> AttachedFiles { get; set; }
    }
}