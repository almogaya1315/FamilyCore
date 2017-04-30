﻿using System;
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
            Create = false;
            Edit = false;
            ManageChat = false;
        }

        [HiddenInput(DisplayValue = false), Range(1, int.MaxValue)]
        public int Id { get; set; }

        [DisplayName("יצירה")]
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

        [DisplayName("עריכה")]
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

        [DisplayName("ניהול צ'ט")]
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

        [DisplayName("מנהל")]
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
