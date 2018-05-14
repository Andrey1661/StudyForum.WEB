using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudyForum.WEB.ViewModels
{
    public class AuthorViewModel
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; }
        //public string AvatarFilePath { get; set; }
    }
}