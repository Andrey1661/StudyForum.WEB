using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyForum.DataAccess.Core.Models
{
    public class UploadFileModel
    {
        public Stream FileStream { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public int ContentLength { get; set; }
        public Guid UploaderId { get; set; }
    }
}
