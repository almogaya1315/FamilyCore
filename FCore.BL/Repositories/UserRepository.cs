using FCore.Common.Interfaces;
using FCore.DAL.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCore.BL.Repositories
{
    public class UserRepository : IUserRepository
    {
        IUserContext UserDB { get; set; }

        public IUserContext CreateUserContext(string connectionStringName)
        {
            return UserDB = new UserContext(connectionStringName);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
