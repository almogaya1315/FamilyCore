namespace FCore.DAL.Identity
{
    using Common.Interfaces;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class UserContext : IdentityDbContext<UserEntity>, IUserContext
    {
        public UserContext(string connectionStringName)
            : base(connectionStringName) { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}