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
    public class MessageModel
    {
        [HiddenInput(DisplayValue = false), Range(1, int.MaxValue)]
        public int Id { get; set; }

        [HiddenInput(DisplayValue = false), Range(1, int.MaxValue)]
        public int GroupId { get; set; }

        [DisplayName("שם קבוצה")]
        public ChatGroupModel Group { get; set; }

        [HiddenInput(DisplayValue = false), Range(1, int.MaxValue)]
        public int SenderId { get; set; }

        [DisplayName("שולח")]
        public FamilyMemberModel Sender { get; set; }

        [HiddenInput(DisplayValue = false), Range(1, int.MaxValue)]
        public int RecieverId { get; set; }

        [DisplayName("נמען")]
        public FamilyMemberModel Reciever { get; set; }

        [DisplayName("תוכן")]
        [Required(ErrorMessage = "שדה חובה"), StringLength(300)]
        public string Content { get; set; }
    }
}
