using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudyForum.DataAccess.Core.Models;

namespace StudyForum.WEB.ViewModels
{
    public class CreateThemeViewModel
    {
        public string Title { get; set; }
        public Guid SubjectId { get; set; }
        public string Content { get; set; }
        public ICollection<HttpPostedFileBase> AttachedFiles { get; set; }
    }
}