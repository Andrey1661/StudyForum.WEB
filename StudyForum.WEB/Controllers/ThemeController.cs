using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using StudyForum.DataAccess.Core.Abstract.Services;
using StudyForum.DataAccess.Core.Models;
using StudyForum.WEB.Utils;
using StudyForum.WEB.ViewModels;

namespace StudyForum.WEB.Controllers
{
    public class ThemeController : Controller
    {
        private IThemeService ThemeService { get; }
        private IMessageService MessageService { get; }
        private IMapper Mapper { get; set; }

        public ThemeController(IThemeService themeService, IMessageService messageService, IMapper mapper)
        {
            ThemeService = themeService;
            MessageService = messageService;
            Mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> Theme(Guid themeId)
        {
            var theme = await ThemeService.GetThemeAsync(themeId);
            var messages = await MessageService.GetThemeMessagesAsync(themeId);

            var model = Mapper.Map<ThemeModel, ThemeViewModel>(theme);
            model.Messages = Mapper.Map<ICollection<MessageModel>, ICollection<MessageViewModel>>(messages);

            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Themes(Guid subjectId)
        {
            ViewBag.SubjectId = subjectId;

            var themes = await ThemeService.GetThemesForSubjectAsync(subjectId);
            var model = Mapper.Map<ICollection<ThemeModel>, ICollection<ThemeViewModel>>(themes);

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
        public async Task<ActionResult> Create(CreateThemeViewModel model)
        {
            var themeId =
                await ThemeService.CreateThemeAsync(model.SubjectId, User.GetId(), model.Title, model.Description);
            return RedirectToAction("Theme", new {themeId});
        }
    }
}