using FCore.Common.Models.Contacts;
using FCore.Common.Models.Families;
using FCore.Common.Models.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCore.Common.Interfaces
{
    public interface ICoreRepository
    {
        ICollection<FamilyModel> GetFamilies();

        FamilyModel GetFamily(int id);
        FamilyModel GetFamily(string name);
    }
}
