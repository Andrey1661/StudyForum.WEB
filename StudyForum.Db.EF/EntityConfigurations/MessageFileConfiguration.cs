using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudyForum.Entities;

namespace StudyForum.Db.EF.EntityConfigurations
{
    internal class MessageFileConfiguration : EntityTypeConfiguration<MessageFile>
    {
        public MessageFileConfiguration()
        {
            HasKey(t => new { t.MessageId, t.FileId });
            HasRequired(t => t.Message)
                .WithMany()
                .HasForeignKey(t => t.MessageId)
                .WillCascadeOnDelete(true);
            HasRequired(t => t.File)
                .WithMany(t => t.Messages)
                .HasForeignKey(t => t.FileId)
                .WillCascadeOnDelete(true);
        }
    }
}
