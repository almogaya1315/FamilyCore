using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using FCore.Common.Interfaces;
using FCore.BL.Repositories;
using FCore.BL;

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
                const string connectionStringName = "name=UserContext";
                app = userRepo.CreateUserContext(app, connectionStringName);
                app = userRepo.CreateUserStore(app);
                app = userRepo.CreateuserManager(app);
            }
        }
    }
}
