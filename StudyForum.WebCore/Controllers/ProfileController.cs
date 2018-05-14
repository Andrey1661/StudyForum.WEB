using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyForum.DataAccess.Core.Abstract.Services;
using StudyForum.DataAccess.Core.Enviroment.Filters;
using StudyForum.DataAccess.Core.Models;
using StudyForum.WebCore.Utils;
using StudyForum.WebCore.ViewModels;
using StudyForum.WebCore.ViewModels.Themes;

namespace StudyForum.WebCore.Controllers
{
    public class ProfileController : Controller
    {
        [Authorize]
        [HttpGet("api/profile")]
        [ProducesResponseType(typeof(ProfileViewModel), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Profile(
            [FromServices]IUserSevice userService, 
            [FromServices]ISemesterService semesterService, 
            [FromServices]IThemeService themeService, 
            [FromServices]ISubjectService subjectService, 
            [FromServices]IGroupService groupService,
            [FromServices]IMapper mapper)
        {
            var user = await userService.GetUserByIdAsync(User.GetId());

            if (user == null)
            {
                return NotFound();
            }

            var semester = await semesterService.GetCurrentSemesterAsync();
            var themes = await themeService.GetThemesAsync(new ThemeFilter { AuthorId = user.Id });

            var model = mapper.Map<UserModel, ProfileViewModel>(user);

            if (user.GroupId.HasValue)
            {
                var subjects = await subjectService.GetSubjectsAsync(user.GroupId.Value, semester.Id);
                var group = await groupService.GetGroupAsync(user.GroupId.Value);

                model.Subjects = mapper.Map<ICollection<SubjectModel>, ICollection<SubjectViewModel>>(subjects);
                model.Group = mapper.Map<GroupModel, GroupViewModel>(group);
            }

            model.Themes = mapper.Map<ICollection<ThemeModel>, ICollection<ThemeViewModel>>(themes);

            return Ok(model);
        }
    }
}
