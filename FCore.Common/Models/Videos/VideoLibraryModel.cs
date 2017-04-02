using FCore.Common.Models.Families;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FCore.Common.Models.Videos
{
    public class VideoLibraryModel
    {
        public VideoLibraryModel()
        {
            Videos = new List<VideoModel>();
        }

        [HiddenInput(DisplayValue = false), Range(1, int.MaxValue)]
        public int Id { get; set; }

        [DisplayName("שם ספריה")]
        [Required(ErrorMessage = "שדה חובה"), StringLength(30)]
        public string Name { get; set; }

        [HiddenInput(DisplayValue = false), Range(1, int.MaxValue)]
        public int FamilyId { get; set; }

        [DisplayName("משפחה")]
        public FamilyModel Family { get; set; }

        [DisplayName("משפחה")]
        [Required(ErrorMessage = "שדה חובה"), StringLength(40)]
        public string FamilyName { get; set; }

        [DisplayName("סרטונים")]
        public virtual ICollection<VideoModel> Videos { get; set; }

        [DisplayName("סך סרטונים")]
        public int? VideosCount { get { return Videos.Count; } }
    }
}
