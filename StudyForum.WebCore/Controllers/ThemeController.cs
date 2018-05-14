using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pagination;
using StudyForum.DataAccess.Core.Abstract.Services;
using StudyForum.DataAccess.Core.Models;
using StudyForum.WebCore.ViewModels;
using StudyForum.WebCore.ViewModels.Themes;

namespace StudyForum.WebCore.Controllers
{
    public class ThemeController : Controller
    {
        private IThemeService ThemeService { get; }
        private IMessageService MessageService { get; }
        private IMapper Mapper { get; }

        public ThemeController(IThemeService themeService, IMessageService messageService, IMapper mapper)
        {
            ThemeService = themeService;
            MessageService = messageService;
            Mapper = mapper;
        }

        [Authorize]
        [HttpGet("/api/themes/{subjectId}")]
        [ProducesResponseType(typeof(ICollection<ThemeViewModel>), 200)]
        public async Task<IActionResult> Themes(Guid subjectId, int page = 0, int pageSize = 10)
        {
            var themes = await ThemeService.GetThemesForSubjectAsync(subjectId);
            var model = Mapper.Map<ICollection<ThemeModel>, ICollection<ThemeViewModel>>(themes);

            return Ok(model);
        }

        [HttpGet("/api/theme/{themeId}")]
        [ProducesResponseType(typeof(ThemeViewModel), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Theme(Guid themeId)
        {
            var theme = await ThemeService.GetThemeAsync(themeId);
            if (theme == null) return NotFound();

            var model = Mapper.Map<ThemeModel, ThemeViewModel>(theme);
            return Ok(model);
        }

        [HttpGet("/api/theme/{themeId}/messages")]
        [ProducesResponseType(typeof(PagedList<MessageViewModel>), 200)]
        public async Task<IActionResult> ThemeMessages(Guid themeId, int page = 0, int pageSize = 10)
        {
            var messages = await MessageService.GetThemeMessagesAsync(themeId, new ListOptions(page, pageSize));
            var model = Mapper.Map<PagedList<MessageModel>, PagedList<MessageViewModel>>(messages);

            return Ok(model);
        }
    }
}
