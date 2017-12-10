using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudyForum.Entities;

namespace StudyForum.Db.EF.EntityConfigurations
{
    internal class UserIdentityConfiguration : EntityTypeConfiguration<UserIdentity>
    {
        public UserIdentityConfiguration()
        {
            HasKey(t => t.Id);
            HasRequired(t => t.User)
                .WithRequiredDependent(t => t.Identity)
                .WillCascadeOnDelete(true);
            HasIndex(t => t.Email)
                .IsUnique(true)
                .IsClustered(false)
                .HasName("UserIdentityEmail_Index");
            Property(t => t.Email)
                .IsRequired()
                .HasMaxLength(100);
            HasRequired(t => t.Role)
                .WithMany()
                .HasForeignKey(t => t.RoleId)
                .WillCascadeOnDelete(false);
        }
    }
}
