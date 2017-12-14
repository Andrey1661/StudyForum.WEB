using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudyForum.DataAccess.Core.Enviroment;
using StudyForum.DataAccess.Core.Models;

namespace StudyForum.DataAccess.Core.Abstract.Services
{
    public interface IRoleService
    {
        Task<RoleModel> GetRoleAsync(Guid id);
        Task<RoleModel> GetRoleAsync(string name);
        Task<PagedList<RoleModel>> GetRolesAsync(ListOptions listOptions);
    }
}
