using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudyForum.Entities;

namespace StudyForum.Db.EF.EntityConfigurations
{
    internal class MessageConfiguration : EntityTypeConfiguration<Message>
    {
        public MessageConfiguration()
        {
            HasKey(t => t.Id);
            HasRequired(t => t.Author)
                .WithMany(t => t.Messages)
                .HasForeignKey(t => t.AuthorId)
                .WillCascadeOnDelete(false);
            HasOptional(t => t.ParentMessage)
                .WithMany(t => t.ChildMessages)
                .HasForeignKey(t => t.ParentMessageId)
                .WillCascadeOnDelete(false);
            HasMany(t => t.ChildMessages)
                .WithOptional(t => t.ParentMessage)
                .HasForeignKey(t => t.ParentMessageId)
                .WillCascadeOnDelete(false);
            HasRequired(t => t.Theme)
                .WithMany(t => t.Messages)
                .HasForeignKey(t => t.ThemeId)
                .WillCascadeOnDelete(true);
            HasMany(t => t.Files)
                .WithRequired(t => t.Message)
                .HasForeignKey(t => t.MessageId)
                .WillCascadeOnDelete(true);
        }
    }
}
