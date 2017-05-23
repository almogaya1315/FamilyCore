using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCore.DAL.Identity
{
    [Table("Users", Schema = "dbf")]
    public class UserEntity : IdentityUser
    {
        public int MemberId { get; set; }

        public int FamilyId { get; set; }

        public string FullName { get; set; }
    }
}
