using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyForum.DataAccess.Core.Models
{
    public class RepositoryModel
    {
        public Guid Id { get; set; }
        public ICollection<FileModel> Files { get; set; }
    }
}
