using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Pagination;
using StudyForum.DataAccess.Core.Abstract.Services;
using StudyForum.DataAccess.Core.Models;
using StudyForum.DataAccess.Utils;
using StudyForum.Db.EF;
using StudyForum.Entities;

namespace StudyForum.DataAccess.Services
{
    public class GroupService : ServiceBase, IGroupService
    {
        public GroupService(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<PagedList<GroupModel>> GetGroupsAsync(string searchString, ListOptions listOptions = null)
        {
            Expression<Func<Group, bool>> predicate = g => true;
            if (string.IsNullOrEmpty(searchString))
                predicate.AndAlso(g => g.Name.ToLower().StartsWith(searchString.ToLower()));

            var query = Context.Groups.OrderBy(g => g.Name).Where(predicate).AsQueryable();
            int count = await Context.Groups.CountAsync(predicate);

            var list = listOptions.CreatePagedList<GroupModel>(count);
            query = listOptions.TakeFrom(query);
            var groups = await query.Select(g => new GroupModel {Id = g.Id, Name = g.Name}).ToListAsync();
            list.AddRange(groups);

            return list;
        }

        public async Task<GroupModel> GetUserGroupAsync(Guid userId)
        {
            var group = await Context.Groups.FirstOrDefaultAsync(g => g.Users.Select(u => u.Id).Contains(userId));
            return group == null ? null : new GroupModel {Id = group.Id, Name = group.Name};
        }

        public async Task<GroupModel> GetGroupAsync(Guid groupId)
        {
            var group = await Context.Groups.FindAsync(groupId);
            return group == null ? null : new GroupModel { Id = group.Id, Name = group.Name };
        }
    }
}
