using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FCore.Common.Models.Members
{
    public class PermissionsModel
    {
        public PermissionsModel()
        {
            Admin = false;
        }

        [HiddenInput(DisplayValue = false), Range(1, int.MaxValue)]
        public int Id { get; set; }

        bool _create;
        [DisplayName("יצירה")]
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
        [DisplayName("עריכה")]
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
        [DisplayName("ניהול צ'ט")]
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
        [DisplayName("מנהל")]
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
