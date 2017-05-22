namespace FCore.DAL.Identity
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class UserContext : DbContext
    {
        public UserContext()
            : base("name=UserContext")
        {
        }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}