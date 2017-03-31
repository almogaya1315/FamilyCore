using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCore.DAL.Entities
{
    [Table("Families", Schema = "dbf")]
    public class FamilyEntity
    {
        public FamilyEntity()
        {
            FamilyMembers = new List<FamilyMemberEntity>();
        }

        [Key]
        public int Id { get; set; }

        [StringLength(40)]
        public string Name { get; set; }

        public virtual ICollection<FamilyMemberEntity> FamilyMembers { get; set; }

        public int? MembersCount { get { return FamilyMembers.Count; } }

        public virtual ICollection<ContactBookEntity> ContactBooks { get; set; }

        public virtual ICollection<AlbumEntity> Albums { get; set; }

        public virtual ICollection<VideoLibraryEntity> VideoLibraries { get; set; }
    }
}
