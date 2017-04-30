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

        public bool Create
        {
            get
            {
                return Create;
            }
            set
            {
                if (value == false)
                {
                    Admin = value;
                }
                else
                {
                    Create = value;
                    if (Edit == value && Create == value)
                    {
                        Admin = value;
                    }
                }
            }
        }

        public bool Edit
        {
            get
            {
                return Edit;
            }
            set
            {
                if (value == false)
                {
                    Admin = value;
                }
                else
                {
                    Edit = value;
                    if (ManageChat == value && Create == value)
                    {
                        Admin = value;
                    }
                }
            }
        }

        public bool ManageChat
        {
            get
            {
                return ManageChat;
            }
            set
            {
                if (value == false)
                {
                    Admin = value;
                }
                else
                {
                    ManageChat = value;
                    if (Edit == value && Create == value)
                    {
                        Admin = value;
                    }
                }
            }
        }

        public bool Admin
        {
            get
            {
                return Admin;
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
                Admin = value;
            }
        }
    }
}
