using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudyForum.WEB.ViewModels
{
    public class ProfileViewModel
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public GroupViewModel Group { get; set; }
        public ICollection<SubjectViewModel> Subjects { get; set; }
        public ICollection<ThemeViewModel> Themes { get; set; }
    }
}