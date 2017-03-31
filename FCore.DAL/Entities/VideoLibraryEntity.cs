using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCore.DAL.Entities
{
    [Table("VideoLibraries", Schema = "dbf"), ComplexType]
    public class VideoLibraryEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int FamilyId { get; set; }
        [ForeignKey("FamilyId")]
        public FamilyEntity Family { get; set; }

        [Required, StringLength(40)]
        public string FamilyName { get; set; }

        public virtual ICollection<VideoEntity> Videos { get; set; }

        public int? VideosCount { get { return Videos.Count; } }
    }
}
