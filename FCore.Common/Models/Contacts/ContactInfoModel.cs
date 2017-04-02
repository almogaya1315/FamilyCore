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
    public class ContactInfoModel
    {
        [HiddenInput(DisplayValue = false), Range(1, int.MaxValue)]
        public int Id { get; set; }

        [HiddenInput(DisplayValue = false), Range(1, int.MaxValue)]
        public int ContactBookId { get; set; }

        [DisplayName("ספר התקשרות")]
        public ContactBookModel ContactBook { get; set; }

        [DisplayName("שם מלא")]
        [Required(ErrorMessage = "שדה חובה"), StringLength(30)]
        public string MemberName { get; set; }

        [DisplayName("מדינה")]
        [StringLength(40), Required(AllowEmptyStrings = true)]
        public string Country { get; set; }

        [DisplayName("עיר")]
        [StringLength(40), Required(AllowEmptyStrings = true)]
        public string City { get; set; }

        [DisplayName("רחוב")]
        [StringLength(40), Required(AllowEmptyStrings = true)]
        public string Street { get; set; }

        [DisplayName("מספר בית")]
        [Range(0, int.MaxValue)]
        public int? HouseNo { get; set; }

        [DisplayName("מספר טלפון")]
        [StringLength(11), Required(AllowEmptyStrings = true)]
        public string PhoneNo { get; set; }

        [DisplayName("אימייל")]
        [StringLength(50), Required(AllowEmptyStrings = true)]
        [RegularExpression(".+\\@.+\\..+")]
        public string Email { get; set; }
    }
}
