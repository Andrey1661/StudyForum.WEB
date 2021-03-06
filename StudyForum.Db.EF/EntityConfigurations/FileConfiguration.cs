﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudyForum.Entities;

namespace StudyForum.Db.EF.EntityConfigurations
{
    internal class FileConfiguration : EntityTypeConfiguration<File>
    {
        public FileConfiguration()
        {
            HasKey(t => t.Id);
            Property(t => t.FileName)
                .IsRequired()
                .HasMaxLength(200);
            HasMany(t => t.Messages)
                .WithRequired(t => t.File)
                .HasForeignKey(t => t.FileId)
                .WillCascadeOnDelete(true);
            HasMany(t => t.Themes)
                .WithRequired(t => t.File)
                .HasForeignKey(t => t.FileId)
                .WillCascadeOnDelete(true);
            HasMany(t => t.Repositories)
                .WithRequired(t => t.File)
                .HasForeignKey(t => t.FileId);
            HasRequired(t => t.Uploader)
                .WithMany()
                .HasForeignKey(t => t.UploaderId);
        }
    }
}
