using FCore.Common.Interfaces;
using FCore.DAL.Entities.Families;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCore.BL.Repositories
{
    public class FCoreRepository : ICoreRepository
    {
        protected FamilyContext CoreDB { get; private set; }
    }
}
