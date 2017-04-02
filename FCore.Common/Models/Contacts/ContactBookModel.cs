using FCore.Common.Models.Families;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FCore.Common.Models.Contacts
{
    public class ContactBookModel
    {
        public ContactBookModel()
        {
            ContactInfoes = new List<ContactInfoModel>();
        }

        [HiddenInput(DisplayValue = false), Range(1, int.MaxValue)]
        public int Id { get; set; }

        [HiddenInput(DisplayValue = false), Range(1, int.MaxValue)]
        public int FamilyId { get; set; }

        [DisplayName("משפחה")]
        public FamilyModel Family { get; set; }

        [DisplayName("משפחה")]
        [Required(ErrorMessage = "שדה חובה"), StringLength(40)]
        public string FamilyName { get; set; }

        [DisplayName("סך נמענים")]
        public int? ContactsCount { get { return ContactInfoes.Count; } }

        [DisplayName("נמענים")]
        public virtual ICollection<ContactInfoModel> ContactInfoes { get; set; }
    }
}
