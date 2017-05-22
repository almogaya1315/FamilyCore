using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCore.DAL.Entities.Users
{
    [Table("Users", Schema = "dbf")]
    public class User : IdentityUser
    {
        public int MemberId { get; set; }

        public int FamilyId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }

        public DateTime BirthDate { get; set; }

        public int Age { get { return DateTime.Now.Year - BirthDate.Year; } }

        public bool IsAdult
        {
            get
            {
                if (Age >= 18)
                    return true;
                else return false;
            }
        }

        public string ProfileImagePath { get; set; }
    }
}
