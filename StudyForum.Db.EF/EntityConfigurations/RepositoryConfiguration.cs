using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudyForum.Entities;

namespace StudyForum.Db.EF.EntityConfigurations
{
    public class RepositoryConfiguration : EntityTypeConfiguration<Repository>
    {
        public RepositoryConfiguration()
        {
            HasKey(t => t.Id);
            HasMany(t => t.Files)
                .WithRequired(t => t.Repository)
                .HasForeignKey(t => t.RepositoryId);
        }
    }
}
