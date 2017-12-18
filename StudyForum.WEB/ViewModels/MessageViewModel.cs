using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudyForum.WEB.ViewModels
{
    public class MessageViewModel
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
        public string AuthorName { get; set; }
    }
}