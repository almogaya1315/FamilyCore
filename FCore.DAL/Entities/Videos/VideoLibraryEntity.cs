using FCore.DAL.Entities.Families;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCore.DAL.Entities.Videos
{
    [Table("VideoLibraries", Schema = "dbf")]
    public class VideoLibraryEntity
    {
        public VideoLibraryEntity()
        {
            Videos = new List<VideoEntity>();
        }

        [Key]
        public int Id { get; set; }

        [Required, StringLength(30)]
        public string Name { get; set; }

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
