using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCore.UI
{
    public class UserMemberStore : UserStore<UserModel>
    {
        readonly IdentityDbContext context;

        public UserMemberStore(IdentityDbContext _context)
        {
            context = _context;
        }
    }
}
