using FCore.BL.Identity.Stores;
using FCore.Identity.DAL;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCore.BL.Identity
{
    public class UserMemberManager : UserManager<UserEntity, string>
    {
        public UserMemberManager(UserMemberStore store) : base(store) { }
    }
}
