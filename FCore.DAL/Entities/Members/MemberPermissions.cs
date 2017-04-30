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
            Admin = false;
        }

        [Key]
        public int Id { get; set; }

        bool _create;
        public bool Create
        {
            get
            {
                return _create;
            }
            set
            {
                if (value == false && _admin != value)
                {
                    _admin = value;
                }
                else
                {
                    _create = value;
                    if (_edit == value && _create == value & _admin != value)
                    {
                        _admin = value;
                    }
                }
            }
        }

        bool _edit;
        public bool Edit
        {
            get
            {
                return _edit;
            }
            set
            {
                if (value == false & _admin != value)
                {
                    _admin = value;
                }
                else
                {
                    _edit = value;
                    if (_chat == value && _create == value & _admin != value)
                    {
                        _admin = value;
                    }
                }
            }
        }

        bool _chat;
        public bool ManageChat
        {
            get
            {
                return _chat;
            }
            set
            {
                if (value == false & _admin != value)
                {
                    _admin = value;
                }
                else
                {
                    _chat = value;
                    if (_edit == value && _create == value & _admin != value)
                    {
                        _admin = value;
                    }
                }
            }
        }

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
                    _create = true;
                    _edit = true;
                    _chat = true;
                }
                else
                {
                    _create = false;
                    _edit = false;
                    _chat = false;
                }
                _admin = value;
            }
        }
    }
}
