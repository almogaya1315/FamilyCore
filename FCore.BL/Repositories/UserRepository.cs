using FCore.BL.Identity;
using FCore.BL.Identity.Stores;
using FCore.Common.Interfaces;
using FCore.Common.Models.Users;
using FCore.DAL.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using FCore.Common.Utils;
using System.Web;

namespace FCore.BL.Repositories
{
    public class UserRepository : RepositoryConverter, IUserRepository 
    {
        UserMemberManager userManager { get; set; }

        public UserRepository() : base(new DbContext(ConstGenerator.UserContextConnectionString)) { }

        public UserRepository(HttpContextBase httpContext) : this()
        {
            userManager = httpContext.GetOwinContext().Get<UserMemberManager>();
        }

        public IAppBuilder CreateUserContext(IAppBuilder app, string connectionStringName)
        {
            return app.CreatePerOwinContext(() => new UserContext(connectionStringName));
        }

        public IAppBuilder CreateUserStore(IAppBuilder app)
        {
            return app.CreatePerOwinContext<UserMemberStore>((opt, cont) => new UserMemberStore(cont.Get<UserContext>()));
        }

        public IAppBuilder CreateUserManager(IAppBuilder app)
        {
            return app.CreatePerOwinContext<UserMemberManager>((opt, cont) => new UserMemberManager(cont.Get<UserMemberStore>()));
        }

        public async Task<IdentityResult> CreateNewUserAsync(UserModel model) // ***
        {
            //var userEntity = ConvertToEntity(model);
            return await userManager.CreateAsync(new UserEntity(model.UserName), model.Password);
        }

        public async Task<UserModel> GetUser(string userName)
        {
            return await ConvertToModel(userManager.FindByNameAsync(userName).Result);
        }

        public void Dispose()
        {
            // todo
            throw new NotImplementedException();
        }
    }
}
