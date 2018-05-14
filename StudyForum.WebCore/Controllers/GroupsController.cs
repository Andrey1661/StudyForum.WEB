using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Pagination;
using StudyForum.DataAccess.Core.Abstract.Services;
using StudyForum.DataAccess.Core.Models;
using StudyForum.WebCore.ViewModels;

namespace StudyForum.WebCore.Controllers
{
    [Route("/api/groups")]
    public class GroupsController : Controller
    {
        [HttpGet]
        [ProducesResponseType(typeof(PagedList<GroupViewModel>), 200)]
        public async Task<IActionResult> Groups([FromServices]IGroupService groupService, [FromServices]IMapper mapper, 
            string query, int page = 0, int? pageSize = null)
        {
            var options = pageSize.HasValue ? new ListOptions(page, pageSize.Value) : null;

            var groups = await groupService.GetGroupsAsync(query, options);
            var model = mapper.Map<ICollection<GroupModel>, ICollection<GroupViewModel>>(groups);
            return Ok(model);
        }
    }
}
