using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudyForum.WEB.ViewModels
{
    public class CreateThemeViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid SubjectId { get; set; }
    }
}