using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCore.DAL.Entities.ChatGroups
{
    [Table("Messages", Schema = "dbf"), ComplexType]
    public class MessageEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int GroupId { get; set; }
        [ForeignKey("GroupId")]
        public ChatGroupEntity Group { get; set; }

        [Required]
        public int SenderId { get; set; }
        [ForeignKey("SenderId")]
        public FamilyMemberEntity Sender { get; set; }

        [Required]
        public int RecieverId { get; set; }
        [ForeignKey("RecieverId")]
        public FamilyMemberEntity Reciever { get; set; }

        [Required, StringLength(300)]
        public string Content { get; set; }
    }
}
