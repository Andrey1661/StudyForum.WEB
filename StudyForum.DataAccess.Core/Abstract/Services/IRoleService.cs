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
    /// <summary>
    /// Интерфейс сервиса, отвечающего за работу с ролями
    /// </summary>
    public interface IRoleService
    {
        /// <summary>
        /// Получает роль по id
        /// </summary>
        /// <param name="id">Id роли</param>
        /// <returns></returns>
        Task<RoleModel> GetRoleAsync(Guid id);

        /// <summary>
        /// Получает роль по имени
        /// </summary>
        /// <param name="name">Имя роли</param>
        /// <returns></returns>
        Task<RoleModel> GetRoleAsync(string name);

        /// <summary>
        /// Получает все роли
        /// </summary>
        /// <param name="listOptions">Параметры для постраничного вывода</param>
        /// <returns></returns>
        Task<PagedList<RoleModel>> GetRolesAsync(ListOptions listOptions);
    }
}
