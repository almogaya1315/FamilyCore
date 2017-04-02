using FCore.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FCore.Common.Models.Members
{
    public class RelativeModel
    {
        public RelativeModel(FamilyMemberModel member, FamilyMemberModel relative, RelationshipType relType) 
        {
            Member = member;
            Relative = relative;
            RelativeId = relative.Id;
            Relationship = relType.ToString();
        }

        [HiddenInput(DisplayValue = false), Range(1, int.MaxValue)]
        public int Id { get; set; }

        [HiddenInput(DisplayValue = false), Range(1, int.MaxValue)]
        public int MemberId { get; set; }

        public FamilyMemberModel Member { get; set; }

        [HiddenInput(DisplayValue = false), Range(1, int.MaxValue)]
        public int RelativeId { get; set; }

        public FamilyMemberModel Relative { get; set; }

        [Required(ErrorMessage = "Required"), DisplayName("קרבה משפחתית")]
        public string Relationship { get; set; }
    }
}
