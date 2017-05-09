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
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int ContactBookId { get; set; }

        [DisplayName("Contact book")]
        public ContactBookModel ContactBook { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int MemberId { get; set; }

        [DisplayName("Full name")]
        public string MemberName { get; set; }

        [StringLength(40), Required(AllowEmptyStrings = true)]
        public string Country { get; set; }

        [StringLength(40), Required(AllowEmptyStrings = true)]
        public string City { get; set; }

        [StringLength(40)]
        public string Street { get; set; }

        [DisplayName("House no.")]
        public int? HouseNo { get; set; }

        [DisplayName("Phone no.")]
        public string PhoneNo { get; set; }

        [StringLength(50)]
        [DisplayName("E-Mail")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Not a valid E-Mail")]
        public string Email { get; set; }
    }
}
