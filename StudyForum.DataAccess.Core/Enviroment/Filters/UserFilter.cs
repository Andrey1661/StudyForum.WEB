using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyForum.DataAccess.Core.Enviroment.Filters
{
    public class UserFilter : ObjectFilterBase
    {
        public Guid? GroupId { get; set; }
        public Guid? RoleId { get; set; }
        public string EmailQuery { get; set; }
        public string NameQuery { get; set; }
        public DateTime? CreateDateBefore { get; set; }
        public DateTime? CreateDateAfter { get; set; }
        public bool? AccountVerified { get; set; }
    }
}
