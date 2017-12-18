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
    public class MessageController : Controller
    {
        private IMessageService MessageService { get; }
        private IMapper Mapper { get; set; }

        public MessageController(IMessageService messageService, IMapper mapper)
        {
            MessageService = messageService;
            Mapper = mapper;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Create(CreateMessageViewModel model)
        {
            if (!ModelState.IsValid)
            {
                throw new NotImplementedException();
            }

            var message = Mapper.Map<CreateMessageViewModel, MessageModel>(model);
            message.AuthorId = User.GetId();

            await MessageService.CreateMessageAsync(message);

            return RedirectToAction("Theme", "Theme", new {themeId = model.ThemeId});
        }
    }
}