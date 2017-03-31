using FCore.DAL.Entities.Families;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCore.DAL.Entities.Albums
{
    [Table("Albums", Schema = "dbf")]
    public class AlbumEntity
    {
        public AlbumEntity()
        {
            Images = new List<ImageEntity>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public int FamilyId { get; set; }
        [ForeignKey("FamilyId")]
        public FamilyEntity Family { get; set; }

        [Required, StringLength(40)]
        public string FamilyName { get; set; }

        public virtual ICollection<ImageEntity> Images { get; set; }

        public int? ImagesCount { get { return Images.Count; } }
    }
}
