using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudyForum.WEB.ViewModels
{
    public class DownloadFileViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int ContentLength { get; set; }
    }
}