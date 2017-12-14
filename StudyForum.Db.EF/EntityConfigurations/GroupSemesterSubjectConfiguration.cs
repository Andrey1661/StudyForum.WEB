using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudyForum.Entities;

namespace StudyForum.Db.EF.EntityConfigurations
{
    internal class GroupSemesterSubjectConfiguration : EntityTypeConfiguration<GroupSemesterSubject>
    {
        public GroupSemesterSubjectConfiguration()
        {
            HasKey(t => t.Id);
            HasRequired(t => t.Subject)
                .WithMany(t => t.GroupSemesters)
                .HasForeignKey(t => t.SubjectId)
                .WillCascadeOnDelete(true);
            HasRequired(t => t.GroupSemester)
                .WithMany(t => t.Subjects)
                .HasForeignKey(t => t.GroupSemesterId)
                .WillCascadeOnDelete(true);
        }
    }
}
