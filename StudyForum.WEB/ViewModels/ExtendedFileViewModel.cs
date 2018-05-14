using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudyForum.WEB.ViewModels
{
    public class ExtendedFileViewModel : DownloadFileViewModel
    {
        public AuthorViewModel Author { get; set; }
        public DateTime UploadDate { get; set; }
    }
}