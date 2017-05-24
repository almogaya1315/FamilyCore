using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCore.Common.Interfaces
{
    public interface IUserRepository : IDisposable
    {
        IAppBuilder CreateUserContext(IAppBuilder app, string connectionStringName);
        IAppBuilder CreateUserStore(IAppBuilder app);
    }
}
