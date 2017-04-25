using FCore.Common.Models.Contacts;
using FCore.Common.Models.Families;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FCore.Common.Models.Members
{
    public class FamilyMemberModel
    {
        public FamilyMemberModel()
        {
            Permissions = new PermissionsModel();
            Relatives = new List<RelativeModel>();
        }

        [HiddenInput(DisplayValue = false), Range(1, int.MaxValue)]
        public int Id { get; set; }

        [HiddenInput(DisplayValue = false), Range(1, int.MaxValue)]
        public int FamilyId { get; set; }

        [DisplayName("משפחה")]
        public FamilyModel Family { get; set; }

        [HiddenInput(DisplayValue = false), Range(1, int.MaxValue)]
        public int PermissionId { get; set; }

        [DisplayName("הרשאות")]
        public PermissionsModel Permissions { get; set; }

        [HiddenInput(DisplayValue = false), Range(1, int.MaxValue)]
        public int ContactInfoId { get; set; }

        [DisplayName("פרטי התקשרות")]
        public ContactInfoModel ContactInfo { get; set; }

        [Required(ErrorMessage = "שדה חובה"), StringLength(30), DisplayName("שם פרטי")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "שדה חובה"), StringLength(40), DisplayName("שם משפחה")]
        public string LastName { get; set; }

        [StringLength(150), DisplayName("על עצמי")]
        public string About { get; set; }

        [DataType(DataType.Date), DisplayName("תאריך לידה")]
        public DateTime? BirthDate { get; set; }

        [Required(AllowEmptyStrings = true), StringLength(100), DisplayName("מקום לידה")]
        public string BirthPlace { get; set; }

        [Range(0, int.MaxValue), DisplayName("גיל")]
        public int? Age { get { return DateTime.Now.Year - BirthDate.Value.Year; } }

        [DisplayName("?האם בגיר")]
        public bool? IsAdult
        {
            get
            {
                if (this.Age >= 18)
                    return true;
                else return false;
            }
        }

        [Required(AllowEmptyStrings = true), DisplayName("תמונת פרופיל"), StringLength(400)]
        public string ProfileImagePath { get; set; }

        [DisplayName("קרובי משפחה")]
        public virtual ICollection<RelativeModel> Relatives { get; set; }
    }
}
