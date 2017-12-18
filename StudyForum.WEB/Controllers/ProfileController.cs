using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using StudyForum.DataAccess.Core.Abstract.Services;
using StudyForum.DataAccess.Core.Enviroment;
using StudyForum.DataAccess.Core.Enviroment.Filters;
using StudyForum.DataAccess.Core.Models;
using StudyForum.WEB.Utils;
using StudyForum.WEB.ViewModels;

namespace StudyForum.WEB.Controllers
{
    public class ProfileController : Controller
    {
        private IMapper Mapper { get; }
        private IUserSevice UserService { get; }
        private ISubjectService SubjectService { get; }
        private IThemeService ThemeService { get; }
        private ISemesterService SemesterService { get; }

        public ProfileController(
            IUserSevice userService, 
            ISubjectService subjectService, 
            IThemeService themeService, 
            ISemesterService semesterService, 
            IMapper mapper)
        {
            UserService = userService;
            SubjectService = subjectService;
            ThemeService = themeService;
            SemesterService = semesterService;
            Mapper = mapper;
        }

        [Authorize]
        public async Task<ActionResult> Index()
        {
            var user = await UserService.GetUserByIdAsync(User.GetId());

            if (user == null)
            {
                return HttpNotFound();
            }

            var semester = await SemesterService.GetCurrentSemesterAsync();
            var subjects = await SubjectService.GetSubjectsAsync(user.GroupId.Value, semester.Id);
            var themes = await ThemeService.GetThemesAsync(new ThemeFilter {AuthorId = user.Id}, new ListOptions());

            var model = Mapper.Map<UserModel, ProfileViewModel>(user);
            model.Subjects = Mapper.Map<PagedList<SubjectModel>, PagedList<SubjectViewModel>>(subjects);
            model.Themes = Mapper.Map<PagedList<ThemeModel>, PagedList<ThemeViewModel>>(themes);

            return View(model);
        }
    }
}