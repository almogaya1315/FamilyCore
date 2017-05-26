namespace FCore.Identity.DAL
{
    using Common.Utils;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class UserContext : IdentityDbContext<UserEntity>
    {
        public UserContext() 
            : this(ConstGenerator.UserContextConnectionString) { }
        public UserContext(string connectionStringName)
            : base(connectionStringName) { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}