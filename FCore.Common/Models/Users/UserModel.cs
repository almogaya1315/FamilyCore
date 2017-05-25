using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCore.Common.Models.Users
{
    public class UserModel : IdentityUser
    {
        public int MemberId { get; set; }

        public int FamilyId { get; set; }

        public string FullName { get; set; }
    }
}
