using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCore.DAL.Entities.Members
{
    [Table("Permissions", Schema = "dbf"), ComplexType]
    public class MemberPermissions<Member>
    {
        public MemberPermissions(Member member)
        {
            CurrentMember = member;
            Create = false;
            Edit = false;
            ManageChat = false;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public int MemberId { get; set; }
        [ForeignKey("MemberId")]
        public Member CurrentMember { get; set; }

        public bool Create { get; set; }

        public bool Edit { get; set; }

        public bool ManageChat { get; set; }
    }
}
