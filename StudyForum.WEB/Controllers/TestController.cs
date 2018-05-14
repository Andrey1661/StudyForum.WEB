using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using StudyForum.DataAccess;

namespace StudyForum.WEB.Controllers
{
    public class TestController : Controller
    {
        private GroupDbInitializer GroupDbInitializer { get; }

        public TestController(GroupDbInitializer groupDbInitializer)
        {
            GroupDbInitializer = groupDbInitializer;
        }

        public async Task<ActionResult> Init()
        {
            await GroupDbInitializer.CreateSemesterData();
            return RedirectToAction("Index", "Home");
        }
    }
}