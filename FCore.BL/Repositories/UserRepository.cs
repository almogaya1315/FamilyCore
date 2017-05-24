using FCore.BL.Stores;
using FCore.Common.Interfaces;
using FCore.DAL.Identity;
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
            return app.CreatePerOwinContext((opt, cont) => new UserMemberStore((cont as IOwinContext).Get<UserContext>));
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
