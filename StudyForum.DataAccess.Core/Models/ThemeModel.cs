using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyForum.DataAccess.Core.Models
{
    public class ThemeModel
    {
        public Guid Id { get; set; }
        public Guid SubjectId { get; set; }
        public Guid AuthorId { get; set; }
        public string Title { get; set; }
    }
}
