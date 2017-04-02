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
            Create = false;
            Edit = false;
            ManageChat = false;
        }

        [HiddenInput(DisplayValue = false), Range(1, int.MaxValue)]
        public int Id { get; set; }

        [DisplayName("יצירה")]
        public bool Create { get; set; }

        [DisplayName("עריכה")]
        public bool Edit { get; set; }

        [DisplayName("ניהול צ'ט")]
        public bool ManageChat { get; set; }
    }
}
