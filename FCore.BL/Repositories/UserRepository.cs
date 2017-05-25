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

namespace FCore.BL.Repositories
{
    public class UserRepository : RepositoryConverter, IUserRepository<UserEntity> 
    {
        UserContext UserDB { get; set; }
        public UserRepository() : base(new DbContext("")) { } // ***
        public UserRepository(DbContext db) : base(db) { }

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

        public Task<IdentityResult> CreateAsync(UserManager<UserEntity> manager, UserModel model)
        {
            var userEntity = ConvertToEntity(model);
            return manager.CreateAsync(userEntity, model.PasswordHash);
        }

        public void Dispose()
        {
            UserDB.Dispose();
        }
    }
}
