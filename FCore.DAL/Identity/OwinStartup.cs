﻿using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

[assembly: OwinStartup(typeof(FCore.DAL.Identity.OwinStartup))]

namespace FCore.DAL.Identity
{
    public class OwinStartup
    {
        public void Configuration(IAppBuilder app)
        {
            //const string connectionString =
            //    @"data source=(LocalDb)\sqldev;initial catalog=FCore.DB.UserIdentity;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
            const string connectionStringName = "name=UserContext";
            app.CreatePerOwinContext(() => new UserContext(connectionStringName));
            app.CreatePerOwinContext<PasswordStore>((opt, cont) => new PasswordStore(cont.Get<UserContext>()));
            app.CreatePerOwinContext<UserMemberManager>((opt, cont) => new UserMemberManager(cont.Get<PasswordStore>()));
        }
    }
}