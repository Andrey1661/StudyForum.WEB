using System;
using System.Threading.Tasks;
using StudyForum.DataAccess.Core.Enviroment;
using StudyForum.DataAccess.Core.Enviroment.Filters;
using StudyForum.DataAccess.Core.Models;

namespace StudyForum.DataAccess.Core.Abstract.Services
{
    public interface IThemeService
    {
        Task CreateThemeAsync(Guid subjectId, Guid authorId, string title, string description = null);
        Task UpdateThemeAuthorAsync(Guid themeId, Guid authorId);
        Task UpdateThemeSubjectAsync(Guid themeId, Guid subjectId);
        Task UpdateThemeAsync(ThemeModel theme);
        Task<ThemeModel> GetThemeAsync(Guid themeId);
        Task<PagedList<ThemeModel>> GetThemesAsync(Guid subjectId, ListOptions listOptions = null);
        Task<PagedList<ThemeModel>> GetThemesAsync(ListOptions listOptions, ThemeFilter filter);
    }
}
