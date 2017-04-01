using FCore.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCore.DAL.Entities.Members
{
    [Table("Relatives", Schema = "dbf")]
    public class MemberRelative
    {
        public MemberRelative() { }

        public MemberRelative(FamilyMemberEntity member, FamilyMemberEntity relative, RelationshipType relType) 
            : this()
        {
            Member = member;
            Relative = relative;
            RelativeId = relative.Id;
            Relationship = relType.ToString();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public int MemberId { get; set; }
        [ForeignKey("MemberId")]
        public FamilyMemberEntity Member { get; set; }

        public int RelativeId { get; set; }

        [NotMapped]
        public FamilyMemberEntity Relative { get; set; }

        [Required, StringLength(20)]
        public string Relationship { get; set; }
    }
}
