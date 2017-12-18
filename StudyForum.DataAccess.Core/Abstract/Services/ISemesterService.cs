using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudyForum.DataAccess.Core.Enviroment;
using StudyForum.DataAccess.Core.Enviroment.Filters;
using StudyForum.DataAccess.Core.Models;

namespace StudyForum.DataAccess.Core.Abstract.Services
{
    public interface ISemesterService
    {
        Task<SemesterModel> GetSemesterAsync(Guid semesterId);
        Task<SemesterModel> GetSemesterByNameAsync(string name);
        Task<SemesterModel> GetCurrentSemesterAsync();
        Task<PagedList<SemesterModel>> GetSemestersAsync(ListOptions listOptions, SemesterFilter filter);
    }
}
