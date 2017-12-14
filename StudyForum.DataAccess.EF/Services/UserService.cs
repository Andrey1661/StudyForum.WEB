using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using StudyForum.DataAccess.Core.Abstract.Services;
using StudyForum.DataAccess.Core.Enviroment;
using StudyForum.DataAccess.Core.Enviroment.Filters;
using StudyForum.DataAccess.Core.Models;
using StudyForum.DataAccess.Utils;
using StudyForum.Db.EF;
using StudyForum.Entities;

namespace StudyForum.DataAccess.Services
{
    public class UserService : ServiceBase, IUserSevice
    {
        public UserService(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<Guid> CreateUserAsync(CreateUserModel user)
        {
            var newUser = Mapper.Map<CreateUserModel, User>(user);
            var role = await Context.Roles.FirstOrDefaultAsync(t => t.Name == "User");

            Guid id = Guid.NewGuid();
            newUser.Id = id;
            newUser.Identity.Id = id;
            newUser.Identity.RoleId = role.Id;
            newUser.Identity.RegistrationDate = DateTime.Now;
            newUser.GroupId = null;

            Context.Users.Add(newUser);
            int res = await Context.SaveChangesAsync();
            return res > 0 ? newUser.Id : Guid.Empty;
        }

        public async Task<UserModel> GetUserAsync(string login, string password)
        {
            var user =
                await Context.Users
                    .Include(u => u.Identity.Role)
                    .FirstOrDefaultAsync(t => t.Identity.Email.Equals(login) && t.Identity.PasswordHash.Equals(password));

            return user == null ? null : Mapper.Map<User, UserModel>(user);
        }

        public async Task<UserModel> GetUserByIdAsync(Guid userId)
        {
            var user = await Context.Users.Include(u => u.Identity).FirstOrDefaultAsync(t => t.Id == userId);

            return user == null ? null : Mapper.Map<User, UserModel>(user);
        }

        public async Task<UserModel> GetUserByEmailAsync(string email)
        {
            var user =
                await Context.Users.Include(u => u.Identity).FirstOrDefaultAsync(t => t.Identity.Email.Equals(email));

            return user == null ? null : Mapper.Map<User, UserModel>(user);
        }

        public async Task<PagedList<UserModel>> GetUsersAsync(ListOptions listOptions, UserFilter filter)
        {
            var users = Context.Users.Include(u => u.Identity).AsQueryable();
            Expression<Func<User, bool>> predicate = null;

            if (filter != null)
            {
                if (filter.GroupId.HasValue) predicate = predicate.AndAlso(u => u.GroupId == filter.GroupId);
                if (filter.RoleId.HasValue) predicate = predicate.AndAlso(u => u.Identity.RoleId == filter.RoleId);
                if (filter.AccountVerified.HasValue)
                    predicate.AndAlso(u => u.Identity.EmailConfirmed == filter.AccountVerified);
                if (filter.CreateDateAfter.HasValue)
                    predicate = predicate.AndAlso(u => u.Identity.RegistrationDate >= filter.CreateDateAfter);
                if (filter.CreateDateAfter.HasValue)
                    predicate = predicate.AndAlso(u => u.Identity.RegistrationDate <= filter.CreateDateBefore);
                if (string.IsNullOrEmpty(filter.EmailQuery))
                    predicate = predicate.AndAlso(u => u.Identity.Email.Contains(filter.EmailQuery));
                if (string.IsNullOrEmpty(filter.NameQuery))
                    predicate =
                        predicate.AndAlso(u => $"{u.SecondName} {u.FirstName} {u.Patronymic}".Contains(filter.NameQuery));
            }

            var result = new PagedList<UserModel>();

            if (predicate != null)
            {
                users = users.Where(predicate);
                result.TotalItemCount = await Context.Users.CountAsync(predicate);
            }
            else
            {
                result.TotalItemCount = await Context.Users.CountAsync();
            }

            if (listOptions != null)
            {
                users = users.Skip(listOptions.Offset).Take(listOptions.PageSize);
                result.Page = listOptions.Page;
                result.PageSize = listOptions.PageSize;
            }

            var userList = await users.ToListAsync();
            result.AddRange(Mapper.Map<IEnumerable<User>, IEnumerable<UserModel>>(userList));
            return result;
        }
    }
}
