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
        private IGroupService GroupService { get; }

        public ProfileController(
            IMapper mapper, 
            IUserSevice userService, 
            ISubjectService subjectService, 
            IThemeService themeService, 
            ISemesterService semesterService, 
            IGroupService groupService)
        {
            Mapper = mapper;
            UserService = userService;
            SubjectService = subjectService;
            ThemeService = themeService;
            SemesterService = semesterService;
            GroupService = groupService;
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
            var themes = await ThemeService.GetThemesAsync(new ThemeFilter {AuthorId = user.Id});

            var model = Mapper.Map<UserModel, ProfileViewModel>(user);

            if (user.GroupId.HasValue)
            {
                var subjects = await SubjectService.GetSubjectsAsync(user.GroupId.Value, semester.Id);
                var group = await GroupService.GetGroupAsync(user.GroupId.Value);

                model.Subjects = Mapper.Map<PagedList<SubjectModel>, PagedList<SubjectViewModel>>(subjects);
                model.Group = Mapper.Map<GroupModel, GroupViewModel>(group);
            }

            model.Themes = Mapper.Map<PagedList<ThemeModel>, PagedList<ThemeViewModel>>(themes);

            return View(model);
        }
    }
}