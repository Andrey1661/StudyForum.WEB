using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudyForum.Entities;

namespace StudyForum.Db.EF.EntityConfigurations
{
    internal class SubjectConfiguration : EntityTypeConfiguration<Subject>
    {
        public SubjectConfiguration()
        {
            HasKey(t => t.Id);
            Property(t => t.Name).IsRequired().HasMaxLength(250);
            HasMany(t => t.GroupSemesters)
                .WithRequired(t => t.Subject)
                .HasForeignKey(t => t.SubjectId)
                .WillCascadeOnDelete(true);
        }
    }
}
