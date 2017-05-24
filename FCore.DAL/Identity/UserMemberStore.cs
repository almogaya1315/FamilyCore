using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCore.DAL.Identity
{
    public class UserMemberStore : IUserStore<UserEntity, string>
    {
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

        public Task<UserEntity> FindByIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<UserEntity> FindByNameAsync(string userName)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(UserEntity user)
        {
            throw new NotImplementedException();
        }
    }
}
