using System;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using StudyForum.DataAccess.Core.Abstract.Services;
using StudyForum.DataAccess.Core.Models;
using StudyForum.WEB.Utils;
using StudyForum.WEB.ViewModels;

namespace StudyForum.WEB.Controllers.Api
{
    public class MessageApiController : ApiController
    {
        private IMessageService MessageService { get; }
        private IMapper Mapper { get; set; }

        public MessageApiController(IMessageService messageService, IMapper mapper)
        {
            MessageService = messageService;
            Mapper = mapper;
        }

        [HttpPost]
        [Authorize]
        public async Task<IHttpActionResult> Create(CreateMessageViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var message = Mapper.Map<CreateMessageViewModel, CreateMessageModel>(model);
            message.AuthorId = User.GetId();

            await MessageService.CreateMessageAsync(message);

            return Ok(model.ThemeId);
        }
    }
}