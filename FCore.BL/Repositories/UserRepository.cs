﻿using FCore.BL.Identity;
using FCore.BL.Identity.Stores;
using FCore.Common.Interfaces;
using FCore.Common.Models.Users;
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
using FCore.DAL.Entities.Families;
using FCore.Identity.DAL;
using FCore.BL.Identity.Managers;
using System.Security.Claims;
using SimpleInjector;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace FCore.BL.Repositories
{
    public class UserRepository : RepositoryConverter, IUserRepository 
    {
        UserMemberManager userManager { get; set; }
        LoginManager loginManager { get; set; }
        UserContext userDB { get; set; }

        public UserRepository() : base(new UserContext(ConstGenerator.UserContextConnectionString)) { }

        //public UserRepository(HttpContextBase httpContext) : this()
        //{
        //    userManager = httpContext.GetOwinContext().Get<UserMemberManager>();
        //    loginManager = httpContext.GetOwinContext().Get<LoginManager>();
        //}

        public UserRepository(UserMemberManager _userManager, LoginManager _loginManager) : this()
        {
            userManager = _userManager;
            loginManager = _loginManager;
        }

        #region DI
        public Container RegisterContext(Container container, string connectionStringName)
        {
            container.Register(() => new UserContext(connectionStringName), Lifestyle.Scoped);
            return container;
        }

        public Container RegisterUserStore(Container container)
        {
            container.Register(() => new UserMemberStore(container.GetInstance<UserContext>()), Lifestyle.Scoped);
            return container;
        }

        public Container RegisterUserManager(Container container)
        {
            container.Register(() => 
            {
                userManager = new UserMemberManager(container.GetInstance<UserMemberStore>());
                // userManager.UserValidator = new UserValidator<UserEntity>(userManager)
                //     { RequireUniqueEmail = true, AllowOnlyAlphanumericUserNames = true };
                userManager.PasswordValidator = new PasswordValidator()
                {
                    RequireDigit = true,
                    RequireLowercase = true,
                    RequireUppercase = true,
                    RequireNonLetterOrDigit = true,
                    RequiredLength = 5
                };
                return userManager;
            }, Lifestyle.Scoped);
            return container;
        }

        public Container RegisterSignInManager(Container container)
        {
            container.Register<LoginManager>(Lifestyle.Scoped);
            return container;
        }

        public IAppBuilder CreateUserManagerFromDependency(IAppBuilder app)
        {
            return app.CreatePerOwinContext<UserMemberManager>(()
                => DependencyResolver.Current.GetService<UserMemberManager>());
        }
        #endregion

        #region identityUserDB
        public async Task<IdentityResult> CreateNewUserAsync(UserModel model) 
        {
            var user = (UserEntity)ConvertToEntity(model);
            return await userManager.CreateAsync(user, user.Password);
        }

        public async Task<IdentityResult> ValidatePassword(string password)
        {
            return await userManager.PasswordValidator.ValidateAsync(password);
        }

        public async Task<SignInStatus> PasswordLoginAsync(UserModel model)
        {
            if (!string.IsNullOrWhiteSpace(model.PasswordHash))
            {
                var hashResult = userManager.PasswordHasher.VerifyHashedPassword(model.PasswordHash, model.Password);
                if (hashResult == PasswordVerificationResult.Failed) throw new Exception(); // todo..
            }
            return await loginManager.PasswordSignInAsync(model.UserName, model.Password, true, true);
        }

        public bool LogOut(HttpContextBase context)
        {
            try
            {
                context.Request.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public async Task<UserModel> GetUserByIdAsync(string id)
        {
            Task<UserEntity> asyncUserEntity = userManager.FindByIdAsync(id);
            if (asyncUserEntity.Result != null) return await ConvertToModel(asyncUserEntity.Result);
            else return null;
        }
        public async Task<UserModel> GetUserByUsrenameAsync(string userName)
        {
            Task<UserEntity> asyncUserEntity = userManager.FindByNameAsync(userName);
            if (asyncUserEntity.Result != null) return await ConvertToModel(asyncUserEntity.Result);
            else return null;
        }
        #endregion

        public void Dispose()
        {
            if (userDB != null) userDB.Dispose();
        }
    }
}
