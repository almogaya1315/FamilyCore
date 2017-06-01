using FCore.BL.Repositories;
using FCore.Common.Utils;
using Microsoft.Owin;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FCore.UI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            ConfigureContainer();
        }

        static void ConfigureContainer()
        {
            using (var userRepo = new UserRepository())
            {
                var container = new Container();
                container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

                container = userRepo.RegisterContext(container, ConstGenerator.UserContextConnectionString);
                container = userRepo.RegisterUserStore(container);
                container = userRepo.RegisterUserManager(container);
                container = userRepo.RegisterSignInManager(container);

                container.Register(() => container.IsVerifying
                ? new OwinContext().Authentication
                : HttpContext.Current.GetOwinContext().Authentication,
                Lifestyle.Scoped);

                container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
                container.Verify();

                DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
            }
        }
    }
}
