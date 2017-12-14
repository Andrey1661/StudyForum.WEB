using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using StudyForum.DataAccess.Core.Abstract.Services;
using StudyForum.DataAccess.Core.Models;
using StudyForum.WEB.ViewModels;

namespace StudyForum.WEB.Controllers
{
    public class AccountController : Controller
    {
        private IUserSevice UserService { get; }
        private IMapper Mapper { get; }
        private ISignInManager SignInManager { get; }

        public AccountController(IUserSevice userService, ISignInManager signInManager, IMapper mapper)
        {
            UserService = userService;
            Mapper = mapper;
            SignInManager = signInManager;
        }

        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SignIn(SignInViewModel model)
        {
            var user = await UserService.GetUserAsync(model.Email, model.Password);

            if (user == null)
            {
                ModelState.AddModelError("", "Неверный логин или пароль");
                return View(model);
            }

            SignInManager.SignIn(user);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SignUp(RegisterViewModel model)
        {
            await UserService.CreateUserAsync(Mapper.Map<RegisterViewModel, CreateUserModel>(model));
            return RedirectToAction("Index", "Home");
        }
    }
}