using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyForum.Entities
{
    public class GroupSemester
    {
        public Guid Id { get; set; }
        public Guid GroupId { get; set; }
        public Guid SemesterId { get; set; }
        public virtual Group Group { get; set; }
        public virtual Semester Semester { get; set; }
        public virtual ICollection<GroupSemesterSubject> Subjects { get; set; }
    }
}
