using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pagination;
using StudyForum.DataAccess.Core.Enviroment;
using StudyForum.DataAccess.Core.Models;

namespace StudyForum.DataAccess.Core.Abstract.Services
{
    public interface IGroupService
    {
        Task<PagedList<GroupModel>> GetGroupsAsync(string searchString, ListOptions listOptions = null);
        Task<GroupModel> GetUserGroupAsync(Guid userId);
        Task<GroupModel> GetGroupAsync(Guid groupId);
    }
}
