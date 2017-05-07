using FCore.Common.Models.Contacts;
using FCore.Common.Models.Families;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using ExpressiveAnnotations.Attributes;

namespace FCore.Common.Models.Members
{
    public class FamilyMemberModel // : IdentityUser
    {
        /*
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<FamilyMemberModel> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
        */

        public FamilyMemberModel()
        {
            Permissions = new PermissionsModel();
            Relatives = new List<RelativeModel>();
        }

        [HiddenInput(DisplayValue = false), Range(1, int.MaxValue)]
        public int Id { get; set; }

        [HiddenInput(DisplayValue = false), Range(1, int.MaxValue)]
        public int FamilyId { get; set; }

        public FamilyModel Family { get; set; }

        [HiddenInput(DisplayValue = false), Range(1, int.MaxValue)]
        public int PermissionId { get; set; }

        public virtual PermissionsModel Permissions { get; set; }

        [HiddenInput(DisplayValue = false), Range(1, int.MaxValue)]
        public int ContactInfoId { get; set; }

        [DisplayName("Contact info")]
        public virtual ContactInfoModel ContactInfo { get; set; }

        [Required(ErrorMessage = "Required field"), StringLength(30), DisplayName("First name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Required field"), StringLength(40), DisplayName("Last name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Required field", AllowEmptyStrings = false)]
        [StringLength(150), DisplayName("About my self")]
        public string About { get; set; }

        [Required(ErrorMessage = "Required field")]
        public string Gender { get; set; }

        [DataType(DataType.Date), DisplayName("Birth date")]
        public DateTime? BirthDate { get; set; }

        [Required(AllowEmptyStrings = true), StringLength(100), DisplayName("Birth place")]
        public string BirthPlace { get; set; }

        [Range(0, int.MaxValue)]
        public int? Age
        {
            get
            {
                if (BirthDate != null)
                    return DateTime.Now.Year - BirthDate.Value.Year;
                else return null;
            }
        }

        [DisplayName("Is adult?")]
        public bool? IsAdult
        {
            get
            {
                if (this.Age >= 18)
                    return true;
                else return false;
            }
        }

        [HiddenInput(DisplayValue = false)]
        [Required(AllowEmptyStrings = true, ErrorMessage = "You must put in a profile picture"), DisplayName("Profile picture"), StringLength(400)]
        public string ProfileImagePath { get; set; }

        public virtual ICollection<RelativeModel> Relatives { get; set; }
    }
}
