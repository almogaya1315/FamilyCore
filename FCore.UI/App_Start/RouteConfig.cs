using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FCore.UI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "LoginPage",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Acount", action = "LoginPage", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "RegisterPage",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Acount", action = "RegisterPage", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "MainPage",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "FamilyCore", action = "Main", id = UrlParameter.Optional }
            );
        }
    }
}
