using FCore.BL.Repositories;
using FCore.Common.Enums;
using FCore.Common.Interfaces;
using FCore.Common.Models.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        public ActionResult LoadProfileImage(FamilyMemberModel creator)
        {
            using (repo = new FCoreRepository())
            {
                //ViewData.Model = 
                //Session[""] = 
                //TempData[""] =
                //Application

                return PartialView("AddProfileImage", repo.GetFamilyMember(creator.Id));
            }
        }

        [HttpPost]
        public ActionResult AddProfileImage(int Id, HttpPostedFileBase ProfileImagePath) // Id => the creator's id
        {
            using (repo = new FCoreRepository())
            {
                string defaultPath = "~/Images/Defualt/profile_defualt.jpg";
                string path = repo.GetFilePath(ProfileImagePath);

                if (ProfileImagePath != null && ProfileImagePath.ContentType.Contains("image") && path != defaultPath)
                {
                    Session["HBFB_file"] = ProfileImagePath;
                    Session["filepath"] = path;
                    Session["filename"] = ProfileImagePath.FileName;

                    repo.UpdateMemberProfileImage(-1, ProfileImagePath, false);

                    Response.StatusCode = (int)HttpStatusCode.OK;
                    return Json(new { success = true }); // PartialView("AddPersonalInfo", repo.GetFamilyMember(Id));
                }
                else if ((HttpPostedFileBase)Session["HBFB_file"] != null)
                {
                    Response.StatusCode = (int)HttpStatusCode.OK;
                    return Json(new { success = true });
                }
                else
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new { success = false });
                } 
            }
        }

        public ActionResult LoadPersonalInfo(FamilyMemberModel creator)
        {
            using (repo = new FCoreRepository())
            {
                ViewData["relenum"] = repo.GetChildRelationshipTypes();
                ViewData["genenum"] = Enum.GetNames(typeof(GenderType)).ToList();
                return PartialView("AddPersonalInfo", repo.GetFamilyMember(creator.Id));
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