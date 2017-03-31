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
    [Table("Relatives", Schema = "dbf"), ComplexType]
    public class MemberRelationships<Member, Relative>
    {
        public MemberRelationships()
        {
            Relatives = new List<Relative>();
        }

        [Key]
        public int MemberId { get; set; }

        [NotMapped]
        public Member CurrentMember { get; set; }

        [Required]
        public int RelativeId { get; set; }
        [ForeignKey("RelativeId")]
        public Relative CurrentRelative { get; set; }

        [NotMapped]
        public RelationshipType RelationshipType { get; set; }

        [Required]
        public string Relationship { get { return RelationshipType.ToString(); } }

        public virtual ICollection<Relative> Relatives { get; set; }
    }
}
