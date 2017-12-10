using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudyForum.Entities;

namespace StudyForum.Db.EF.EntityConfigurations
{
    internal class ThemeFileConfiguration : EntityTypeConfiguration<ThemeFile>
    {
        public ThemeFileConfiguration()
        {
            HasKey(t => new {t.ThemeId, t.FileId});
            HasRequired(t => t.Theme)
                .WithMany()
                .HasForeignKey(t => t.ThemeId)
                .WillCascadeOnDelete(true);
            HasRequired(t => t.File)
                .WithMany(t => t.Themes)
                .HasForeignKey(t => t.FileId)
                .WillCascadeOnDelete(true);
        }
    }
}
