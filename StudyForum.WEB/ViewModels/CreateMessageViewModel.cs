using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudyForum.WEB.ViewModels
{
    public class CreateMessageViewModel
    {
        public Guid ThemeId { get; set; }
        public Guid? ParentMessageId { get; set; }
        public string Content { get; set; }
        public ICollection<HttpPostedFileBase> AttachedFiles { get; set; }
    }
}