using FCore.Common.Models.Users;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FCore.Common.Interfaces
{
    public interface IUserRepository : IDisposable 
    {
        IAppBuilder CreateUserContext(IAppBuilder app, string connectionStringName);
        IAppBuilder CreateUserStore(IAppBuilder app);
        IAppBuilder CreateUserManager(IAppBuilder app);

        Task<IdentityResult> CreateNewUserAsync(UserModel model);

        Task<UserModel> GetUserAsync(string userName);
    }
}
