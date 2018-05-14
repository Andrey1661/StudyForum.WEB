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

        [AllowAnonymous]
        public ActionResult SignIn(string returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> SignIn(SignInViewModel model, string returnUrl = null)
        {
            var user = await UserService.GetUserAsync(model.Email, model.Password);

            if (user == null)
            {
                ModelState.AddModelError("", "Неверный логин или пароль");
                return View(model);
            }

            SignInManager.SignIn(user, model.IsPersistent);

            if (!string.IsNullOrEmpty(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult SignOut()
        {
            SignInManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SignUp(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await UserService.CreateUserAsync(Mapper.Map<RegisterViewModel, CreateUserModel>(model));
            return RedirectToAction("Index", "Home");
        }
    }
}