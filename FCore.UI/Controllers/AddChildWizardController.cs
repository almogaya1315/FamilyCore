using FCore.BL.Repositories;
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

        public ActionResult AddProfileImage(FamilyMemberModel creator, HttpPostedFileBase ProfileImagePath)
        {
            using (repo = new FCoreRepository())
            {
                if (ProfileImagePath != null && ProfileImagePath.ContentType.Contains("image"))
                {
                    ViewData["HBFB_file"] = ProfileImagePath;
                    return PartialView("AddPersonalInfo", repo.GetFamilyMember(creator.Id));
                }
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