using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyForum.DataAccess.Core.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Patronymic { get; set; }
        public RoleModel Role { get; set; }
        public bool AccountConfirmed { get; set; }
        public FileModel AvatarFile { get; set; }
    }
}
