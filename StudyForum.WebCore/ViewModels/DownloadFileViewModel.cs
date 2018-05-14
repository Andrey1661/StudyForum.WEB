using System;

namespace StudyForum.WebCore.ViewModels
{
    public class DownloadFileViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int ContentLength { get; set; }
    }
}