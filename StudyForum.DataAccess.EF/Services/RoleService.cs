using StudyForum.DataAccess.Core.Abstract.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudyForum.DataAccess.Core.Enviroment;
using StudyForum.DataAccess.Core.Models;
using AutoMapper;
using Pagination;
using StudyForum.Db.EF;

namespace StudyForum.DataAccess.Services
{
    public class RoleService : ServiceBase, IRoleService
    {
        protected RoleService(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<RoleModel> GetRoleAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<RoleModel> GetRoleAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<PagedList<RoleModel>> GetRolesAsync(ListOptions listOptions)
        {
            throw new NotImplementedException();
        }
    }
}
