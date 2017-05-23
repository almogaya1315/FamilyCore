using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCore.DAL.Identity
{
    public class PasswordHandler : IUserPasswordStore<UserEntity, int>
    {
        readonly UserContext context;

        public PasswordHandler(UserContext _context)
        {
            context = _context;
        }

        public Task CreateAsync(UserEntity user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(UserEntity user)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<UserEntity> FindByIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<UserEntity> FindByNameAsync(string userName)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetPasswordHashAsync(UserEntity user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasPasswordAsync(UserEntity user)
        {
            throw new NotImplementedException();
        }

        public Task SetPasswordHashAsync(UserEntity user, string passwordHash)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(UserEntity user)
        {
            throw new NotImplementedException();
        }
    }
}
