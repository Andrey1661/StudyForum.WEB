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
using StudyForum.Db.EF;
using StudyForum.Entities;

namespace StudyForum.DataAccess.Services
{
    public class ThemeService : ServiceBase, IThemeService
    {
        public ThemeService(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<Guid> CreateThemeAsync(Guid subjectId, Guid authorId, string title, string description = null)
        {
            var theme = new Theme
            {
                Id = Guid.NewGuid(),
                CreationDate = DateTime.Now,
                AuthorId = authorId,
                Title = title,
                Description = description,
                SubjectId = subjectId
            };

            Context.Themes.Add(theme);
            await Context.SaveChangesAsync();
            return theme.Id;
        }

        public async Task UpdateThemeAuthorAsync(Guid themeId, Guid authorId)
        {
            var theme = await Context.Themes.FindAsync(themeId);
            if (theme == null) return;

            theme.AuthorId = authorId;
            Context.Entry(theme).State = EntityState.Modified;
            await Context.SaveChangesAsync();
        }

        public async Task UpdateThemeSubjectAsync(Guid themeId, Guid subjectId)
        {
            var theme = await Context.Themes.FindAsync(themeId);
            if (theme == null) return;

            theme.SubjectId = subjectId;
            Context.Entry(theme).State = EntityState.Modified;
            await Context.SaveChangesAsync();
        }

        public async Task UpdateThemeAsync(ThemeModel theme)
        {
            var storedTheme = await Context.Themes.FindAsync(theme.Id);
            if (storedTheme == null) return;

            storedTheme.AuthorId = theme.Author.Id;
            storedTheme.SubjectId = theme.SubjectId;
            storedTheme.Title = theme.Title;
            storedTheme.Description = theme.Description;

            Context.Entry(storedTheme).State = EntityState.Modified;
            await Context.SaveChangesAsync();
        }

        public async Task<ThemeModel> GetThemeAsync(Guid themeId)
        {
            var theme = await Context.Themes.Include(t => t.Author).FirstOrDefaultAsync(t => t.Id == themeId);
            return theme == null ? null : Mapper.Map<Theme, ThemeModel>(theme);
        }

        public async Task<PagedList<ThemeModel>> GetUserThemesAsync(Guid userId, ListOptions listOptions = null)
        {
            var themes = Context.Themes.AsQueryable();
            var result = new PagedList<ThemeModel>();

            if (listOptions != null)
            {
                themes = themes.OrderBy(t => t.CreationDate).Skip(listOptions.Offset).Take(listOptions.PageSize);
                result.Page = listOptions.Page;
                result.PageSize = listOptions.PageSize;
            }

            var themeList = await themes.Where(t => t.AuthorId == userId).ToListAsync();
            result.AddRange(Mapper.Map<IEnumerable<Theme>, IEnumerable<ThemeModel>>(themeList));
            return result;
        }

        public async Task<PagedList<ThemeModel>> GetThemesForSubjectAsync(Guid subjectId, ListOptions listOptions)
        {
            var themes = Context.Themes.AsQueryable();
            var result = new PagedList<ThemeModel>();

            if (listOptions != null)
            {
                themes = themes.OrderBy(t => t.CreationDate).Skip(listOptions.Offset).Take(listOptions.PageSize);
                result.Page = listOptions.Page;
                result.PageSize = listOptions.PageSize;
            }

            var themeList = await themes.Where(t => t.SubjectId == subjectId).ToListAsync();
            result.AddRange(Mapper.Map<IEnumerable<Theme>, IEnumerable<ThemeModel>>(themeList));
            return result;
        }

        public Task<PagedList<ThemeModel>> GetThemesAsync(ListOptions listOptions = null)
        {
            return GetThemesAsync(null, listOptions);
        }

        public async Task<PagedList<ThemeModel>> GetThemesAsync(ThemeFilter filter, ListOptions listOptions)
        {
            var themes = Context.Themes.AsQueryable();

            if (filter != null)
            {
                if (filter.AuthorId.HasValue) themes = themes.Where(t => t.AuthorId == filter.AuthorId);
                if (filter.SubjectId.HasValue) themes = themes.Where(t => t.SubjectId == filter.SubjectId);
                if (filter.CreateDateAfter.HasValue) themes = themes.Where(t => t.CreationDate >= filter.CreateDateAfter);
                if (filter.CreateDateBefore.HasValue) themes = themes.Where(t => t.CreationDate <= filter.CreateDateBefore);
                if (string.IsNullOrEmpty(filter.SearchString))
                    themes =
                        themes.Where(
                            t => t.Title.Contains(filter.SearchString) || t.Description.Contains(filter.SearchString));
            }

            var result = new PagedList<ThemeModel>();

            if (listOptions != null)
            {
                themes = themes.OrderBy(t => t.CreationDate).Skip(listOptions.Offset).Take(listOptions.PageSize);
                result.Page = listOptions.Page;
                result.PageSize = listOptions.PageSize;
            }

            var themeList = await themes.ToListAsync();
            result.AddRange(Mapper.Map<IEnumerable<Theme>, IEnumerable<ThemeModel>>(themeList));
            return result;
        }
    }
}
