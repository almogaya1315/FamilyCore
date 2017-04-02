using FCore.Common.Models.Families;
using FCore.Common.Models.Members;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FCore.Common.Models.ChatGroups
{
    public class ChatGroupModel
    {
        public ChatGroupModel()
        {
            Messages = new List<MessageModel>();
        }

        [HiddenInput(DisplayValue = false), Range(1, int.MaxValue)]
        public int Id { get; set; }

        [DisplayName("שם קבוצה")]
        [Required(ErrorMessage = "שדה חובה"), StringLength(20)]
        public string Name { get; set; }

        [DisplayName("סך קרובים")]
        public int MemberCount { get { return Messages.Count; } }

        [HiddenInput(DisplayValue = false), Range(1, int.MaxValue)]
        public int FamilyId { get; set; }

        [DisplayName("משפחה")]
        public FamilyModel Family { get; set; }

        [HiddenInput(DisplayValue = false), Range(1, int.MaxValue)]
        public int ManagerId { get; set; }

        [DisplayName("מנהל")]
        public FamilyMemberModel Manager { get; set; }

        [DisplayName("הודעות")]
        public virtual ICollection<MessageModel> Messages { get; set; }
    }
}
