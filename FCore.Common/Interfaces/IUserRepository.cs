using FCore.Common.Models.Users;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using SimpleInjector;
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
        Task<IdentityResult> CliamTest();

        Container RegisterContext(Container container, string connectionStringName);
        Container RegisterUserStore(Container container);
        Container RegisterUserManager(Container container);
        Container RegisterSignInManager(Container container);

        IAppBuilder CreateUserManagerFromDependency(IAppBuilder app);
        IAppBuilder CreateUserContext(IAppBuilder app, string connectionStringName);
        IAppBuilder CreateUserStore(IAppBuilder app);
        IAppBuilder CreateUserManager(IAppBuilder app);
        IAppBuilder CreateLoginManager(IAppBuilder app);

        Task<IdentityResult> CreateNewUserAsync(UserModel model);
        Task<IdentityResult> ValidatePassword(string password);
        Task<SignInStatus> PasswordLoginAsync(UserModel model);
        Task<UserModel> GetUserAsync(string userName);
    }
}
