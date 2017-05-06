using FCore.BL.Repositories;
using FCore.Common.Enums;
using FCore.Common.Interfaces;
using FCore.Common.Models.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FCore.UI.Controllers
{
    public class AddChildWizardController : Controller
    {
        ICoreRepository repo { get; set; }

        public ActionResult AddChild(FamilyMemberModel creator)
        {
            using (repo = new FCoreRepository())
            {
                return View(repo.GetFamilyMember(creator.Id));
            }
        }

        [HttpPost]
        public ActionResult AddProfileImage(FamilyMemberModel creator, HttpPostedFileBase ProfileImagePath)
        {
            using (repo = new FCoreRepository())
            {
                if (ProfileImagePath != null && ProfileImagePath.ContentType.Contains("image"))
                {
                    ViewData["HBFB_file"] = ProfileImagePath;
                    ViewData["filepath"] = repo.GetFilePath(ProfileImagePath);
                    ViewData["filename"] = ProfileImagePath.FileName;
                    ViewData["relenum"] = repo.GetChildRelationshipTypes();
                    ViewData["genenum"] = Enum.GetNames(typeof(GenderType)).ToList();

                    repo.UpdateMemberProfileImage(-1, ProfileImagePath, false);

                    return PartialView("AddPersonalInfo", repo.GetFamilyMember(creator.Id));
                }
                //else if ((HttpPostedFileBase)ViewData["HBFB_file"] != null)
                //{

                //}
                else return PartialView();
            }
        }

        public ActionResult AddPersonalInfo(FamilyMemberModel postedMember)
        {
            HttpPostedFileBase file = (HttpPostedFileBase)ViewData["HBFB_file"];

            using (repo = new FCoreRepository())
            {
                if (ModelState.IsValid)
                {
                    ViewData["personal_info"] = postedMember = repo.SetPersonalInfo(postedMember, repo.GetFilePath(file));
                    return PartialView("AddContactInfo", repo.GetFamilyMember(postedMember.Id).ContactInfo);
                }
                else return PartialView(postedMember);
            }
        }
    }
}