using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCore.DAL.Identity
{
    public class UserMemberStore : UserStore<UserEntity>
    {
        readonly UserContext context;

        public UserMemberStore(UserContext _context)
        {
            context = _context;
        }
    }
}
