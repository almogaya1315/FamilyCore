using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FCore.Common.Models.Families
{
    public class FamilyModel
    {
        public FamilyModel()
        {
            //FamilyMembers = new List<FamilyMemberModel>();
            //ContactBooks = new List<ContactBookModel>();
            //Albums = new List<AlbumModel>();
            //VideoLibraries = new List<VideoLibraryModel>();
            //ChatGroups = new List<ChatGroupModel>();
        }

        [HiddenInput(DisplayValue = false), Range(1, int.MaxValue)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Required"), StringLength(40)]
        public string Name { get; set; }

        [DisplayName("Family Members")]
        public ICollection<FamilyMemberModel> FamilyMembers { get; set; }

        [DisplayName("Total Members"), Range(0, int.MaxValue)]
        public int? MembersCount { get { return FamilyMembers.Count; } }

        //[DisplayName("Contact Books")]
        //public ICollection<ContactBookModel> ContactBooks { get; set; }

        //public ICollection<AlbumModel> Albums { get; set; }

        //[DisplayName("Video Libraries")]
        //public ICollection<VideoLibraryModel> VideoLibraries { get; set; }

        //[DisplayName("Chat Groups")]
        //public ICollection<ChatGroupModel> ChatGroups { get; set; }
    }
}
