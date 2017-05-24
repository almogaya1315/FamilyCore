using FCore.Common.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCore.Common.Interfaces
{
    public interface IUserContext : IdentityDbContext<UserModel>
    {
    }
}
