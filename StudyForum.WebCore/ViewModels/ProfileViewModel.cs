using System.Collections.Generic;
using StudyForum.WebCore.ViewModels.Themes;

namespace StudyForum.WebCore.ViewModels
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