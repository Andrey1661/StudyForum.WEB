using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyForum.Entities
{
    public class GroupSemesterSubject
    {
        public Guid Id { get; set; }
        public Guid SubjectId { get; set; }
        public virtual Subject Subject { get; set; }
        public Guid GroupSemesterId { get; set; }
        public virtual GroupSemester GroupSemester { get; set; }
        public virtual ICollection<Theme> Themes { get; set; }
    }
}
