using FCore.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCore.Common.Utils
{
    public static class StaticRepository<Repository> where Repository : ICoreRepository, new()
                                                     
    {
        static ICoreRepository _familyCoreRepository;
        public static ICoreRepository FamilyCoreRepository
        {
            get
            {
                if (_familyCoreRepository == null)
                    return _familyCoreRepository = new Repository();
                else return _familyCoreRepository;
            }
        }
    }
}
