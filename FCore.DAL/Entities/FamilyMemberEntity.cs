using FCore.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCore.DAL.Entities
{
    [Table("FamilyMembers", Schema = "dbf"), ComplexType]
    public class FamilyMemberEntity
    {
        public FamilyMemberEntity()
        {
            Relatives = new Dictionary<FamilyMemberEntity, RelationshipType>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public int FamilyId { get; set; }
        [ForeignKey("FamilyId")]
        public FamilyEntity Family { get; set; }

        [Required]
        public int ContactInfoId { get; set; }
        [ForeignKey("ContactInfoId")]
        public ContactInfoEntity ContactInfo { get; set; }

        [Required, StringLength(30)]
        public string FirstName { get; set; }

        [Required, StringLength(40)]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

        [Required(AllowEmptyStrings = true), StringLength(100)]
        public string BirthPlace { get; set; }

        [Range(0, int.MaxValue)]
        public int? Age { get { return DateTime.Now.Year - BirthDate.Value.Year; } }

        public bool? IsAdult
        {
            get
            {
                if (this.Age >= 18)
                    return true;
                else return false;
            }
        }

        [Required(AllowEmptyStrings = true), StringLength(400)]
        public string ProfileImagePath { get; set; }

        public virtual Dictionary<FamilyMemberEntity, RelationshipType> Relatives { get; set; }
    }
}
