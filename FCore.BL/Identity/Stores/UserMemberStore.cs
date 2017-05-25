using FCore.DAL.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCore.BL.Identity.Stores
{
    public class UserMemberStore : UserStore<UserEntity>
    {
        protected IdentityDbContext context { get; private set; }

        public UserMemberStore(UserContext _context) : base(_context)
        {
            context = _context;
        }
    }
}
