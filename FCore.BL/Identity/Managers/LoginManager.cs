using FCore.Identity.DAL;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCore.BL.Identity.Managers
{
    public class LoginManager : SignInManager<UserEntity, string>
    {
        public LoginManager(UserMemberManager userManager, IAuthenticationManager authentication)
            : base(userManager, authentication) { }
    }
}
