using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace StudyForum.WEB
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "CreateTheme",
                url: "CreateTheme/{subjectId}",
                defaults: new { controller = "Theme", action = "Create" }
            );

            routes.MapRoute(
                name: "Theme",
                url: "Theme/{themeId}",
                defaults: new {controller = "Theme", action = "Theme"}
            );

            routes.MapRoute(
                name: "Themes",
                url: "Themes/{subjectId}",
                defaults: new {controller = "Theme", action = "Themes"}
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new {controller = "Home", action = "Index", id = UrlParameter.Optional}
            );
        }
    }
}
