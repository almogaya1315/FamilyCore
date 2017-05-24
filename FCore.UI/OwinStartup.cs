using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using FCore.Common.Identity;
using FCore.Common.Interfaces;
using FCore.BL.Repositories;

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
                app.CreatePerOwinContext(() => userRepo.CreateUserContext(connectionStringName)); //new UserContext(connectionStringName));
                app.CreatePerOwinContext<UserMemberStore>((opt, cont) => new UserMemberStore(cont.Get<UserContext>()));
                app.CreatePerOwinContext<UserMemberManager>((opt, cont) => new UserMemberManager(cont.Get<UserMemberStore>()));
            }
        }
    }
}
