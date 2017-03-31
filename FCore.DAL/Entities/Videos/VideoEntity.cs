using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCore.DAL.Entities.Videos
{
    [Table("Videos",Schema = "dbf"), ComplexType]
    public class VideoEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Libraryid { get; set; }
        [ForeignKey("Libraryid")]
        public VideoLibraryEntity Library { get; set; }

        [StringLength(50), Required(AllowEmptyStrings = true)]
        public string Description { get; set; }

        [StringLength(100), Required]
        public string Path { get; set; }
    }
}
