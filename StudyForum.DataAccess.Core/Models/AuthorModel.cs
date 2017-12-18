using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyForum.DataAccess.Core.Models
{
    public class AuthorModel
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public RoleModel Role { get; set; }
        public FileModel AvatarFile { get; set; }
    }
}
