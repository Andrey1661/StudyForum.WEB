using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudyForum.WEB.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MainProfile()
        {
            return View();
        }

        public ActionResult Topic()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Topics()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}