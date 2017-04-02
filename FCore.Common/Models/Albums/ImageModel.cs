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
    public class ImageModel
    {
        [HiddenInput(DisplayValue = false), Range(1, int.MaxValue)]
        public int Id { get; set; }

        [HiddenInput(DisplayValue = false), Range(1, int.MaxValue)]
        public int AlbumId { get; set; }

        [DisplayName("שם אלבום")]
        public AlbumModel Album { get; set; }

        [DisplayName("תיאור")]
        [StringLength(50), Required(ErrorMessage = "שדה חובה")]
        public string Description { get; set; }

        [DisplayName("קובץ")]
        [StringLength(100), Required(ErrorMessage = "שדה חובה")]
        public string Path { get; set; }
    }
}
