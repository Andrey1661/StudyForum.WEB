using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudyForum.Entities;

namespace StudyForum.Db.EF.EntityConfigurations
{
    internal class ThemeConfiguration : EntityTypeConfiguration<Theme>
    {
        public ThemeConfiguration()
        {
            HasKey(t => t.Id);
            Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(100);
            Property(t => t.Description)
                .HasMaxLength(1000);
            HasRequired(t => t.Author)
                .WithMany(t => t.Themes)
                .HasForeignKey(t => t.AuthorId)
                .WillCascadeOnDelete(false);
            HasRequired(t => t.Subject)
                .WithMany(t => t.Themes)
                .HasForeignKey(t => t.SubjectId)
                .WillCascadeOnDelete(true);
            HasMany(t => t.Messages)
                .WithRequired(t => t.Theme)
                .HasForeignKey(t => t.ThemeId)
                .WillCascadeOnDelete(true);
            HasMany(t => t.Files)
                .WithRequired(t => t.Theme)
                .HasForeignKey(t => t.ThemeId)
                .WillCascadeOnDelete(false);
        }
    }
}
