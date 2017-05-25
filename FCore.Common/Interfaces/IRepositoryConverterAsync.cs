using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCore.Common.Interfaces
{
    public interface IRepositoryConverterAsync<UserModel, UserEntity>
    {
        Task<UserModel> ConvertToModel(UserEntity entity);
        // Task<UserEntity> ConvertToEntity(UserModel entity);
    }
}
