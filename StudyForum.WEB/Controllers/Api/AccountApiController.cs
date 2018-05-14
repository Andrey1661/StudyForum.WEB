using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
using System.Web.Mvc;
using Newtonsoft.Json;
using StudyForum.DataAccess.Core.Abstract.Services;
using StudyForum.DataAccess.Services;
using StudyForum.WEB.ViewModels;

namespace StudyForum.WEB.Controllers.Api
{
    public class AccountApiController : ApiController
    {
        private IUserSevice UserService { get; }
        private ISignInManager SignInManager { get; }
        private IGroupService GroupService { get; }

        public AccountApiController(IUserSevice userService, ISignInManager signInManager, IGroupService groupService)
        {
            UserService = userService;
            SignInManager = signInManager;
            GroupService = groupService;
        }

        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.HttpGet]
        public async Task<JsonResult<List<GroupViewModel>>> GetGroupList(string searchPattern, int maxCount)
        {
            var data = (await GroupService.GetGroupsAsync(searchPattern, maxCount)).Select(g => new GroupViewModel
            {
                Id = g.Id,
                Name = g.Name
            }).ToList();

            return Json(data);
        }

        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> SignIn(SignInViewModel model)
        {
            var user = await UserService.GetUserAsync(model.Email, model.Password);

            if (user == null)
            {
                ModelState.AddModelError("", "Неверный логин или пароль");
                return BadRequest(ModelState);
            }

            SignInManager.SignIn(user, model.IsPersistent);

            return Ok();
        }
    }
}