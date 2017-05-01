using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCore.Common.Interfaces
{
    public interface IPermission
    {
        int Id { get; set; }

        bool Create { get; set; }

        bool Edit { get; set; }

        bool ManageChat { get; set; }

        bool Admin { get; set; }
    }
}
