using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace StudyForum.WEB.Controllers
{
    public class SubjectController : Controller
    {
        public async Task<ActionResult> Subjects(Guid groupId)
        {
            return View();
        }
    }
}