using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudyForum.Entities;

namespace StudyForum.Db.EF.EntityConfigurations
{
    internal class SemesterConfiguration : EntityTypeConfiguration<Semester>
    {
        public SemesterConfiguration()
        {
            HasKey(t => t.Id);
            Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);
            HasMany(t => t.GroupSemesters)
                .WithRequired(t => t.Semester)
                .HasForeignKey(t => t.SemesterId)
                .WillCascadeOnDelete(true);
        }
    }
}
