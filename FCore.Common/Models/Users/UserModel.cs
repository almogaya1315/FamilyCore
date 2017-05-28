using FCore.Common.Models.Members;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCore.Common.Models.Users
{
    public class UserModel : IdentityUser
    {
        public int MemberId { get; set; }

        public int FamilyId { get; set; }

        public string FullName { get; set; }

        [Required(ErrorMessage = "Required!")]
        //[MinLength(5, ErrorMessage = "At least 5 letters")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Required!")]
        [MinLength(3, ErrorMessage = "At least 3 letters")]
        public override string UserName
        {
            get { return base.UserName; }
            set { base.UserName = value; }
        }

        public FamilyMemberModel Member { get; set; }
    }
}
