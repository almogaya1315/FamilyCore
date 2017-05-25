using FCore.Common.Models.Users;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCore.Common.Interfaces
{
    public interface IUserRepository<TEntity> : IDisposable where TEntity : IdentityUser
    {
        IAppBuilder CreateUserContext(IAppBuilder app, string connectionStringName);
        IAppBuilder CreateUserStore(IAppBuilder app);
        IAppBuilder CreateUserManager(IAppBuilder app);

        Task<IdentityResult> CreateAsync(UserManager<TEntity> manager, UserModel model);
    }
}
