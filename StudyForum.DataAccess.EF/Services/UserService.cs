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
        private IFileService FileService { get; }

        public UserService(ApplicationDbContext context, IMapper mapper, IFileService fileService) : base(context, mapper)
        {
            FileService = fileService;
        }

        public async Task<Guid> CreateUserAsync(CreateUserModel user, string roleName = "user")
        {
            var newUser = Mapper.Map<CreateUserModel, User>(user);
            var role = await Context.Roles.FirstOrDefaultAsync(t => t.Name.ToLower() == roleName);

            var hash = new PasswordHash(user.Password);

            Guid id = Guid.NewGuid();
            newUser.Id = id;
            newUser.Identity.Id = id;
            newUser.Identity.RoleId = role.Id;
            newUser.Identity.RegistrationDate = DateTime.Now;
            newUser.Identity.PasswordHash = Encoding.Default.GetString(hash.ToArray());
            newUser.GroupId = user.GroupId;

            var repository = new Repository
            {
                Id = Guid.NewGuid(),
                OwnerType = 1,
                OwnerId = id,
                Shared = false
            };
            newUser.RepositoryId = repository.Id;

            Context.Users.Add(newUser);
            Context.Repositories.Add(repository);
            int res = await Context.SaveChangesAsync();
            return res > 0 ? newUser.Id : Guid.Empty;
        }

        public async Task<UserModel> GetUserAsync(string login, string password)
        {
            var user =
                await Context.Users
                    .Include(u => u.Identity.Role)
                    .FirstOrDefaultAsync(t => t.Identity.Email.Equals(login));

            if (user == null) return null;

            var bytes = Encoding.Default.GetBytes(user.Identity.PasswordHash);
            var hash = new PasswordHash(bytes);
            if (!hash.Verify(password)) return null;

            var model = Mapper.Map<User, UserModel>(user);

            if (model != null && user.AvatarId != null)
            {
                model.AvatarFilePath = await FileService.GetFilePathAsync(user.AvatarId.Value);
            }

            return model;
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

            int totalItemCount;

            if (predicate != null)
            {
                users = users.Where(predicate);
                totalItemCount = await Context.Users.CountAsync(predicate);
            }
            else
            {
                totalItemCount = await Context.Users.CountAsync();
            }

            var result = listOptions.CreatePagedList<UserModel>(totalItemCount);
            users = listOptions.TakeFrom(users);

            var userList = await users.ToListAsync();
            result.AddRange(Mapper.Map<IEnumerable<User>, IEnumerable<UserModel>>(userList));
            return result;
        }
    }
}
