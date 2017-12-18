using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyForum.DataAccess.Core.Models
{
    public class SemesterModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime DateBegin { get; set; }
        public DateTime DateEnd { get; set; }
    }
}
