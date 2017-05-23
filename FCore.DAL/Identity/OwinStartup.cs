using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

[assembly: OwinStartup(typeof(FCore.DAL.Identity.OwinStartup))]

namespace FCore.DAL.Identity
{
    public class OwinStartup
    {
        public void Configuration(IAppBuilder app)
        {
            const string connectionString =
                @"data source=(LocalDb)\sqldev;initial catalog=FCore.DB.UserIdentity;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
            app.CreatePerOwinContext(() => new UserContext(connectionString));
            app.CreatePerOwinContext<UserStore<IdentityUser>>((opt, cont) => new UserStore<IdentityUser>(cont.Get<IdentityDbContext>()));
            app.CreatePerOwinContext<UserManager<IdentityUser>>((opt, cont) => new UserManager<IdentityUser>(cont.Get<>(UserStore<IdentityUser>)));
        }
    }
}
