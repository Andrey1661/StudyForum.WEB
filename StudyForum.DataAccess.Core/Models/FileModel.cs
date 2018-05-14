using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyForum.DataAccess.Core.Models
{
    public class FileModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int ContentLength { get; set; }
        public DateTime UploadDate { get; set; }
        public Guid UploaderId { get; set; }
        public AuthorModel Uploader { get; set; }
    }

    public class DownloadFileModel
    {
        public string FileName { get; set; }
        public string FileType { get; set; }
        public int ContentLength { get; set; }
        public Stream FileStream { get; set; }
    }
}
