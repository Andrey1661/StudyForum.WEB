using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace StudyForum.WebCore.ViewModels.Themes
{
    public class CreateThemeViewModel
    {
        public string Title { get; set; }
        public Guid SubjectId { get; set; }
        public string Content { get; set; }
        public ICollection<IFormFile> AttachedFiles { get; set; }
    }
}