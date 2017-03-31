using FCore.DAL.Entities.Albums;
using FCore.DAL.Entities.ChatGroups;
using FCore.DAL.Entities.Contacts;
using FCore.DAL.Entities.Videos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCore.DAL.Entities.Families
{
    [Table("Families", Schema = "dbf"), ComplexType]
    public class FamilyEntity
    {
        public FamilyEntity()
        {
            FamilyMembers = new List<FamilyMemberEntity>();
            ContactBooks = new List<ContactBookEntity>();
            Albums = new List<AlbumEntity>();
            VideoLibraries = new List<VideoLibraryEntity>();
            ChatGroups = new List<ChatGroupEntity>();
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

        public virtual ICollection<ChatGroupEntity> ChatGroups { get; set; }
    }
}
