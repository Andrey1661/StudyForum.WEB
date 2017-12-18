using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyForum.DataAccess.Core.Models
{
    public class MessageModel
    {
        public Guid Id { get; set; }
        public Guid ThemeId { get; set; }
        public Guid AuthorId { get; set; }
        public DateTime CreationDate { get; set; }
        public AuthorModel Author { get; set; }
        public string Content { get; set; }
        public Guid? ParentMessageId { get; set; }
        public ICollection<FileModel> AttachedFiles { get; set; }
    }
}
