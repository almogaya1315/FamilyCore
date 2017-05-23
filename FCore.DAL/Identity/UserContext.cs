namespace FCore.DAL.Identity
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class UserContext : IdentityDbContext<UserEntity>
    {
        public UserContext(string connectionStringName)
            : base("name=UserContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}