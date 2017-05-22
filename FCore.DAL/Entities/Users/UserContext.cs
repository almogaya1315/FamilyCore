namespace FCore.DAL.Entities.Users
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class UserContext : IdentityDbContext<User>
    {
        public UserContext()
            : base("name=UserContext")
        {
        }
    }
}