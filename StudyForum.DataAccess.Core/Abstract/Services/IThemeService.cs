using System;
using System.Threading.Tasks;
using Pagination;
using StudyForum.DataAccess.Core.Enviroment;
using StudyForum.DataAccess.Core.Enviroment.Filters;
using StudyForum.DataAccess.Core.Models;

namespace StudyForum.DataAccess.Core.Abstract.Services
{
    /// <summary>
    /// Интерфейс сервиса, отвечающего за работу с темами
    /// </summary>
    public interface IThemeService
    {
        Task<Guid> CreateThemeAsync(Guid subjectId, Guid authorId, string title);
        Task UpdateThemeAuthorAsync(Guid themeId, Guid authorId);
        Task UpdateThemeSubjectAsync(Guid themeId, Guid subjectId);
        Task UpdateThemeAsync(ThemeModel theme);
        Task<ThemeModel> GetThemeAsync(Guid themeId);
        Task<PagedList<ThemeModel>> GetUserThemesAsync(Guid userId, ListOptions listOptions = null);
        Task<PagedList<ThemeModel>> GetThemesForSubjectAsync(Guid subjectId, ListOptions listOptions = null);
        Task<PagedList<ThemeModel>> GetThemesAsync(ListOptions listOptions = null);
        Task<PagedList<ThemeModel>> GetThemesAsync(ThemeFilter filter, ListOptions listOptions = null);
    }
}
