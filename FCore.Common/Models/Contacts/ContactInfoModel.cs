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

        [DisplayName("Contact book")]
        public ContactBookModel ContactBook { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int MemberId { get; set; }

        [DisplayName("Full name")]
        [Required(ErrorMessage = "Required field"), StringLength(30)]
        public string MemberName { get; set; }

        [StringLength(40), Required(AllowEmptyStrings = true)]
        public string Country { get; set; }

        [StringLength(40), Required(AllowEmptyStrings = true)]
        public string City { get; set; }

        [StringLength(40), Required(AllowEmptyStrings = true)]
        public string Street { get; set; }

        [DisplayName("House no.")]
        [Range(0, int.MaxValue)]
        public int? HouseNo { get; set; }

        [DisplayName("Phone no.")]
        [StringLength(11), Required(AllowEmptyStrings = true)]
        public string PhoneNo { get; set; }

        [DisplayName("E-Mail")]
        [RegularExpression(".+\\@.+\\..+")]
        [StringLength(50), Required(AllowEmptyStrings = true, ErrorMessage = "Not a valid e-mail")]
        public string Email { get; set; }
    }
}
