using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using FCore.Common.Interfaces;
using FCore.BL.Repositories;
using FCore.BL;
using FCore.Common.Models.Users;
using FCore.Common.Utils;
using Microsoft.AspNet.Identity;

[assembly: OwinStartup(typeof(FCore.DAL.Identity.OwinStartup))]

namespace FCore.DAL.Identity
{
    public class OwinStartup
    {
        public void Configuration(IAppBuilder app)
        {
            IUserRepository userRepo;

            using (userRepo = new UserRepository())
            {
                string connectionStringName = ConstGenerator.UserContextConnectionString;
                app = userRepo.CreateUserContext(app, connectionStringName);
                app = userRepo.CreateUserStore(app);
                app = userRepo.CreateUserManager(app);
                app = userRepo.CreateLoginManager(app);

                app.UseCookieAuthentication(new Microsoft.Owin.Security.Cookies.CookieAuthenticationOptions
                    { AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie });
            }
        }
    }
}
