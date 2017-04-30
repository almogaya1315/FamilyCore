using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCore.DAL.Entities.Members
{
    [Table("Permissions", Schema = "dbf")]
    public class MemberPermissions
    {
        public MemberPermissions()
        {
            Create = false;
            Edit = false;
            ManageChat = false;
        }

        [Key]
        public int Id { get; set; }

        public bool Create { get; set; }

        public bool Edit { get; set; }

        public bool ManageChat { get; set; }

        [NotMapped]
        bool _admin;
        public bool Admin
        {
            get
            {
                return _admin;
            }
            set
            {
                if (value == true)
                {
                    Create = true;
                    Edit = true;
                    ManageChat = true;
                }
                else
                {
                    Create = false;
                    Edit = false;
                    ManageChat = false;
                }
                _admin = value;
            }
        }
    }
}
