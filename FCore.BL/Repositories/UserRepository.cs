using FCore.BL.Identity;
using FCore.BL.Identity.Stores;
using FCore.Common.Interfaces;
using FCore.DAL.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCore.BL.Repositories
{
    public class UserRepository : IUserRepository
    {
        UserContext UserDB { get; set; }

        public IAppBuilder CreateUserContext(IAppBuilder app, string connectionStringName)
        {
            return app.CreatePerOwinContext(() => new UserContext(connectionStringName));
        }

        public IAppBuilder CreateUserStore(IAppBuilder app)
        {
            return app.CreatePerOwinContext<UserMemberStore>((opt, cont) => new UserMemberStore(cont.Get<UserContext>()));
        }

        public IAppBuilder CreateuserManager(IAppBuilder app)
        {
            return app.CreatePerOwinContext<UserMemberManager>((opt, cont) => new UserMemberManager(cont.Get<UserMemberStore>()));
        }

        public void Dispose()
        {
            UserDB.Dispose();
        }
    }
}
