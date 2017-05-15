using FCore.DAL.Entities.Contacts;
using FCore.DAL.Entities.Families;
using FCore.DAL.Entities.Members;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCore.DAL.Entities
{
    [Table("FamilyMembers", Schema = "dbf")]
    public class FamilyMemberEntity
    {
        public FamilyMemberEntity()
        {
            Permissions = new MemberPermissions();
            Relatives = new List<MemberRelative>();
        }

        [Key]
        public int Id { get; set; }

        //[Required]
        public int? FamilyId { get; set; }
        [ForeignKey("FamilyId")]
        public FamilyEntity Family { get; set; }

        //[Required]
        public int? PermissionId { get; set; }
        [ForeignKey("PermissionId")]
        public MemberPermissions Permissions { get; set; }

        //[Required]
        public int? ContactInfoId { get; set; }
        [ForeignKey("ContactInfoId")]
        public ContactInfoEntity ContactInfo { get; set; }

        [Required, StringLength(30)]
        public string FirstName { get; set; }

        [Required, StringLength(40)]
        public string LastName { get; set; }

        [StringLength(150)]
        public string About { get; set; }

        [Required]
        public string Gender { get; set; }

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

        public virtual ICollection<MemberRelative> Relatives { get; set; }
    }
}
