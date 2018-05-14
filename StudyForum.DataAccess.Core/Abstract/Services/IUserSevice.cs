using System;
using System.Threading.Tasks;
using Pagination;
using StudyForum.DataAccess.Core.Enviroment;
using StudyForum.DataAccess.Core.Enviroment.Filters;
using StudyForum.DataAccess.Core.Models;

namespace StudyForum.DataAccess.Core.Abstract.Services
{
    /// <summary>
    /// Интерфейс сервиса, отвечающего за работу с пользователями
    /// </summary>
    public interface IUserSevice
    {
        Task<Guid> CreateUserAsync(CreateUserModel user, string roleName = "user");
        Task<UserModel> GetUserAsync(string login, string password);
        Task<UserModel> GetUserByIdAsync(Guid userId);
        Task<UserModel> GetUserByEmailAsync(string email);
        Task<PagedList<UserModel>> GetUsersAsync(ListOptions listOptions, UserFilter filter);
    }
}
