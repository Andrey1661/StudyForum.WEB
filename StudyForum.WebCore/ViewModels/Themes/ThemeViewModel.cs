using System;
using System.Collections.Generic;

namespace StudyForum.WebCore.ViewModels.Themes
{
    public class ThemeViewModel
    {
        public Guid Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string Title { get; set; }
        public AuthorViewModel Author { get; set; }
        public ICollection<MessageViewModel> Messages { get; set; }
    }
}