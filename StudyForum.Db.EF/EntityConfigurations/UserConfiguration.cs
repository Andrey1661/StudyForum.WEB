using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudyForum.Entities;

namespace StudyForum.Db.EF.EntityConfigurations
{
    internal class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            HasKey(t => t.Id);
            HasRequired(t => t.Identity)
                .WithRequiredPrincipal(t => t.User)
                .WillCascadeOnDelete(true);
            Property(t => t.FirstName)
                .HasMaxLength(50);
            Property(t => t.SecondName)
                .HasMaxLength(50);
            Property(t => t.Patronymic)
                .HasMaxLength(50);
            HasOptional(t => t.Group)
                .WithMany(t => t.Users)
                .HasForeignKey(t => t.GroupId)
                .WillCascadeOnDelete(false);
            HasOptional(t => t.Avatar)
                .WithMany()
                .HasForeignKey(t => t.AvatarId)
                .WillCascadeOnDelete(false);
            HasMany(t => t.Themes)
                .WithRequired(t => t.Author)
                .HasForeignKey(t => t.AuthorId)
                .WillCascadeOnDelete(false);
            HasMany(t => t.Messages)
                .WithRequired(t => t.Author)
                .HasForeignKey(t => t.AuthorId)
                .WillCascadeOnDelete(false);
            HasMany(t => t.Files)
                .WithRequired(t => t.Uploader)
                .HasForeignKey(t => t.UploaderId);
            HasOptional(t => t.Repository)
                .WithMany()
                .HasForeignKey(t => t.RepositoryId)
                .WillCascadeOnDelete(false);
        }
    }
}
