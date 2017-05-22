using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCore.DAL.Identity
{
    public class User : IUser<int>
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string PasswordHash { get; set; }
    }
}
