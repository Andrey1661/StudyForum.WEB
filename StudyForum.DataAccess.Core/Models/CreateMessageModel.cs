using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyForum.DataAccess.Core.Models
{
    public class CreateMessageModel
    {
        public Guid ThemeId { get; set; }
        public Guid AuthorId { get; set; }
        public Guid? ParentMessageId { get; set; }
        public string Content { get; set; }
        public ICollection<UploadFileModel> AttachedFiles { get; set; }
    }
}
