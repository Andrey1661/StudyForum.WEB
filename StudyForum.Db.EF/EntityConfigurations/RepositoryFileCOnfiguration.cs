using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudyForum.Entities;

namespace StudyForum.Db.EF.EntityConfigurations
{
    public class RepositoryFileConfiguration : EntityTypeConfiguration<RepositoryFile>
    {
        public RepositoryFileConfiguration()
        {
            HasKey(t => new {t.RepositoryId, t.FileId});
            HasRequired(t => t.Repository)
                .WithMany(t => t.Files)
                .HasForeignKey(t => t.RepositoryId);
            HasRequired(t => t.File)
                .WithMany(t => t.Repositories)
                .HasForeignKey(t => t.FileId);
        }
    }
}
