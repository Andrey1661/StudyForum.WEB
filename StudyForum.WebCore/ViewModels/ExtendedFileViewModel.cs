using System;
using StudyForum.WebCore.ViewModels.Themes;

namespace StudyForum.WebCore.ViewModels
{
    public class ExtendedFileViewModel : DownloadFileViewModel
    {
        public AuthorViewModel Author { get; set; }
        public DateTime UploadDate { get; set; }
    }
}