using System;

namespace StudyForum.WebCore.ViewModels
{
    public class AuthorViewModel
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; }
        //public string AvatarFilePath { get; set; }
    }
}