using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCore.DAL.Entities.Albums
{
    [Table("images", Schema = "dbf"), ComplexType]
    public class ImageEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int AlbumId { get; set; }
        [ForeignKey("AlbumId")]
        public AlbumEntity Album { get; set; }

        [StringLength(50), Required(AllowEmptyStrings = true)]
        public string Description { get; set; }

        [StringLength(100), Required]
        public string Path { get; set; }
    }
}
