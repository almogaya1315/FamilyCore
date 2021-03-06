﻿using FCore.DAL.Entities.Families;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCore.DAL.Entities.ChatGroups
{
    [Table("ChatGroups", Schema = "dbf")]
    public class ChatGroupEntity
    {
        public ChatGroupEntity()
        {
            Messages = new List<MessageEntity>();
        }

        [Key]
        public int Id { get; set; }

        [Required, StringLength(20)]
        public string Name { get; set; }

        public int MemberCount { get { return Messages.Count; } }

        [Required]
        public int FamilyId { get; set; }
        [ForeignKey("FamilyId")]
        public FamilyEntity Family { get; set; }

        [Required]
        public int ManagerId { get; set; }
        [ForeignKey("ManagerId")]
        public FamilyMemberEntity Manager { get; set; }

        public virtual ICollection<MessageEntity> Messages { get; set; }
    }
}
