using FCore.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FCore.Common.Utils
{
    public static class ModelStateHelper
    {
        public static string ValidationMessageColor
        {
            get
            {
                return "color:#8B5FEB;";
            }
        }

        public static ICollection<string> GetModelKeys(ModelStateSet forPage)
        {
            switch (forPage)
            {
                case ModelStateSet.ForInitialInfo:
                    return new List<string>()
                    {
                        "Id", "Member.Id", "MemberId", "FamilyId", "EmailConfirmed", "PhoneNumberConfirmed", "TwoFactorEnabled", "LockoutEnabled", "AccessFailedCount",
                        "Member.FamilyId", "Member.PermissionId", "Member.ContactInfoId", "Member.FirstName",
                        "Member.LastName", "Member.About", "Member.Gender", "Member.BirthPlace", "Member.BirthDate"
                    };
                case ModelStateSet.ForUsernameValidation:
                    return new List<string>()
                    {
                        "Id", "Password", "MemberId", "FamilyId", "EmailConfirmed", "PhoneNumberConfirmed", "TwoFactorEnabled", "LockoutEnabled", "AccessFailedCount",
                        "Member.Id", "Member.FamilyId", "Member.PermissionId", "Member.ContactInfoId", "Member.FirstName",
                        "Member.LastName", "Member.About", "Member.Gender", "Member.BirthPlace", "Member.BirthDate"
                    };
                case ModelStateSet.ForProfileImage:
                    return new List<string>()
                {
                    "FamilyId", "PermissionId", "ContactInfoId", "FirstName", "LastName", "About", "Gender", "BirthPlace", "BirthDate"
                };
                case ModelStateSet.ForPersonalInfo:
                    return new List<string>()
                {
                    "Id", "FamilyId", "PermissionId", "ContactInfoId", "About", "ProfileImagePath"
                };
                case ModelStateSet.ForContactInfo:
                    return new List<string>() { "Email" };
                case ModelStateSet.ForLifeStory:
                    return new List<string>() { "Id", "FamilyId", "PermissionId", "ContactInfoId", "ProfileImagePath" };
                default:
                    throw new InvalidOperationException("The ModelStateSet passed was not implemented in switch.");
            }
        }

        public static ViewDataDictionary SetModelState(ViewDataDictionary viewData, ModelStateDictionary modelState, ModelStateSet forPage)
        {
            switch (forPage)
            {
                case ModelStateSet.ForProfileImage:
                    break;
                case ModelStateSet.ForPersonalInfo:
                    if (modelState.FirstOrDefault(state => state.Key == "FirstName").Value.Errors.Count > 0)
                    {
                        viewData["fnstate"] = true;
                    }
                    if (modelState.FirstOrDefault(state => state.Key == "LastName").Value.Errors.Count > 0)
                    {
                        viewData["lnstate"] = true;
                    }
                    if (modelState.FirstOrDefault(state => state.Key == "BirthDate").Value.Errors.Count > 0) // Value.AttemptedValue == string.Empty)
                    {
                        viewData["bdstate"] = true;
                    }
                    if (modelState.FirstOrDefault(state => state.Key == "BirthPlace").Value.Errors.Count > 0)
                    {
                        viewData["bpstate"] = true;
                    }
                    break;
                case ModelStateSet.ForContactInfo:
                    break;
                case ModelStateSet.ForLifeStory:
                    break;
            }

            return viewData;
        }

        public static ICollection<string> GetPasswordValidationErrors(string errorMessage)
        {
            ICollection<string> errors = new List<string>();
            var error = "- ";
            foreach (var c in errorMessage)
            {
                error += c;
                if (c == '.')
                {
                    errors.Add(error);
                    error = "-";
                }
            }
            return errors;
        }
    }
}
