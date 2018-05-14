using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace StudyForum.WebCore.ViewModels
{
    public class CreateMessageViewModel
    {
        public Guid ThemeId { get; set; }
        public Guid? ParentMessageId { get; set; }
        public string Content { get; set; }
        public ICollection<IFormFile> AttachedFiles { get; set; }
    }
}