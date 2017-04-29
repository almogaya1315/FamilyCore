using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCore.DAL.Entities.Contacts
{
    [Table("ContactInfoes", Schema = "dbf")]
    public class ContactInfoEntity
    {
        [Key]
        public int Id { get; set; }

        public int ContactBookId { get; set; }
        [ForeignKey("ContactBookId")]
        public ContactBookEntity ContactBook { get; set; }

        [Required]
        public int MemberId { get; set; }

        [Required, StringLength(30)]
        public string MemberName { get; set; }

        [StringLength(40), Required(AllowEmptyStrings = true)]
        public string Country { get; set; }

        [StringLength(40), Required(AllowEmptyStrings = true)]
        public string City { get; set; }

        [StringLength(40), Required(AllowEmptyStrings = true)]
        public string Street { get; set; }

        [Range(0, int.MaxValue)]
        public int? HouseNo { get; set; }

        [StringLength(11), Required(AllowEmptyStrings = true)]
        public string PhoneNo { get; set; }

        [StringLength(50), Required(AllowEmptyStrings = true)]
        [RegularExpression(".+\\@.+\\..+")]
        public string Email { get; set; }
    }
}
