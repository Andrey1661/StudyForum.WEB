using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudyForum.Entities;

namespace StudyForum.Db.EF.EntityConfigurations
{
    internal class RoleConfiguration : EntityTypeConfiguration<Role>
    {
        public RoleConfiguration()
        {
            HasKey(t => t.Id);
            Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);
            HasIndex(t => t.Name)
                .IsUnique(true)
                .IsClustered(false)
                .HasName("RoleName_Index");
        }
    }
}
