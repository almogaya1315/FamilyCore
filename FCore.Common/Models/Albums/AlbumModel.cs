using FCore.Common.Models.Families;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FCore.Common.Models.Albums
{
    public class AlbumModel
    {
        public AlbumModel()
        {
            Images = new List<ImageModel>();
        }

        [HiddenInput(DisplayValue = false), Range(1, int.MaxValue)]
        public int Id { get; set; }

        [Required(ErrorMessage = "שדה חובה")]
        [StringLength(30), DisplayName("שם אלבום")]
        public string Name { get; set; }

        [HiddenInput(DisplayValue = false), Range(1, int.MaxValue)]
        public int FamilyId { get; set; }

        [DisplayName("משפחה")]
        public FamilyModel Family { get; set; }

        [DisplayName("משפחה")]
        [Required(ErrorMessage = "שדה חובה"), StringLength(40)]
        public string FamilyName { get; set; }

        [DisplayName("תמונות")]
        public virtual ICollection<ImageModel> Images { get; set; }

        [DisplayName("סך תמונות")]
        public int? ImagesCount { get { return Images.Count; } }
    }
}
