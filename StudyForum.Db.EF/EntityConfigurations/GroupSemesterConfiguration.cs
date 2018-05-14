using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudyForum.Entities;

namespace StudyForum.Db.EF.EntityConfigurations
{
    internal class GroupSemesterConfiguration : EntityTypeConfiguration<GroupSemester>
    {
        public GroupSemesterConfiguration()
        {
            HasKey(t => t.Id);
            HasRequired(t => t.Group)
                .WithMany(t => t.Semesters)
                .HasForeignKey(t => t.GroupId)
                .WillCascadeOnDelete(true);
            HasMany(t => t.Subjects)
                .WithRequired(t => t.GroupSemester)
                .HasForeignKey(t => t.GroupSemesterId)
                .WillCascadeOnDelete(true);
            HasOptional(t => t.Repository)
                .WithMany()
                .HasForeignKey(t => t.RepositoryId);
        }
    }
}
