using System;
using System.Threading.Tasks;
using StudyForum.DataAccess.Core.Enviroment;
using StudyForum.DataAccess.Core.Enviroment.Filters;
using StudyForum.DataAccess.Core.Models;

namespace StudyForum.DataAccess.Core.Abstract.Services
{
    public interface IThemeRepository
    {
        Task CreateThemeAsync(Guid subjectId, Guid authorId, string title);
        Task UpdateThemeAuthorAsync(Guid themeId, Guid authorId);
        Task UpdateThemeSubjectAsync(Guid themeId, Guid subjectId);
        Task UpdateThemeTitleAsync(Guid themeId, string title);
        Task<ThemeModel> GetTheme(Guid themeId);
        Task<PagedList<ThemeModel>> GetThemes(ListOptions listOptions, ThemeFilter filter);
    }
}
