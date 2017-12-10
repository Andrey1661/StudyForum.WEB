using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyForum.DataAccess.Core.Models
{
    public class FileModel
    {
        public Guid Id { get; set; }
        public string PhysicalPath { get; set; }
    }
}
