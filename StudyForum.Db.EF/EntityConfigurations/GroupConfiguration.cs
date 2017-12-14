using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudyForum.Entities;

namespace StudyForum.Db.EF.EntityConfigurations
{
    internal class GroupConfiguration : EntityTypeConfiguration<Group>
    {
        public GroupConfiguration()
        {
            HasKey(t => t.Id);
            Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);
            HasMany(t => t.Users)
                .WithOptional(t => t.Group)
                .HasForeignKey(t => t.GroupId)
                .WillCascadeOnDelete(false);
            HasMany(t => t.Semesters)
                .WithRequired(t => t.Group)
                .HasForeignKey(t => t.GroupId)
                .WillCascadeOnDelete(true);
        }
    }
}
