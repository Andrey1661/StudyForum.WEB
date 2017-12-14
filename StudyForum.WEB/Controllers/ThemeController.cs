using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using StudyForum.DataAccess.Core.Abstract.Services;
using StudyForum.WEB.Utils;
using StudyForum.WEB.ViewModels;

namespace StudyForum.WEB.Controllers
{
    public class ThemeController : Controller
    {
        private IThemeService ThemeService { get; }

        public ThemeController(IThemeService themeService)
        {
            ThemeService = themeService;
        }

        public async Task<ActionResult> Themes(Guid subjectId)
        {
            var themes = await ThemeService.GetThemesAsync(subjectId);
            var model = themes.Select(t => new ThemeViewModel {Title = t.Title, Description = t.Description});

            return View(model);
        }

        [Authorize]
        [HttpGet]
        public ActionResult Create(Guid subjectId)
        {
            return View(new CreateThemeViewModel {SubjectId = subjectId});
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> CreateTheme(CreateThemeViewModel model)
        {
            await ThemeService.CreateThemeAsync(model.SubjectId, User.GetId(), model.Title, model.Description);
            return RedirectToAction("Themes", new {subjectId = model.SubjectId});
        }
    }
}