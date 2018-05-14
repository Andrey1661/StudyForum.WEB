using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pagination;
using StudyForum.DataAccess.Core.Enviroment;
using StudyForum.DataAccess.Core.Enviroment.Filters;
using StudyForum.DataAccess.Core.Models;

namespace StudyForum.DataAccess.Core.Abstract.Services
{
    /// <summary>
    /// Интерфейс сервиса, отвечающего за работу с семестрами
    /// </summary>
    public interface ISemesterService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="semesterId"></param>
        /// <returns></returns>
        Task<SemesterModel> GetSemesterAsync(Guid semesterId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<SemesterModel> GetSemesterByNameAsync(string name);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<SemesterModel> GetCurrentSemesterAsync();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listOptions"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<PagedList<SemesterModel>> GetSemestersAsync(ListOptions listOptions, SemesterFilter filter);
    }
}
