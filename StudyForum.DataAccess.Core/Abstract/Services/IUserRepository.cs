using System;
using System.Threading.Tasks;
using StudyForum.DataAccess.Core.Enviroment;
using StudyForum.DataAccess.Core.Enviroment.Filters;
using StudyForum.DataAccess.Core.Models;

namespace StudyForum.DataAccess.Core.Abstract.Services
{
    public interface IUserRepository
    {
        Task CreateUserAsync(CreateUserModel user);
        Task<UserModel> GetUserAsync(string login, string password);
        Task<UserModel> GetUserByIdAsync(Guid userId);
        Task<UserModel> GetUserByEmailAsync(string email);
        Task<PagedList<UserModel>> GetUsers(ListOptions listOptions, UserFilter filter);
    }
}
