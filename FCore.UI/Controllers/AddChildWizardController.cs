using ExpressiveAnnotations.Attributes;
using FCore.BL.Repositories;
using FCore.Common.Enums;
using FCore.Common.Interfaces;
using FCore.Common.Models.Contacts;
using FCore.Common.Models.Members;
using FCore.Common.Utils;
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
                Session.Clear();
                Session["validcolor"] = ModelStateHelper.ValidationMessageColor;
                return View(repo.GetFamilyMember(creator.Id));
            }
        }

        /// <summary>
        /// This function is called only if the user returns from pi page. The initial get is from the 'AddChild' PartialView.
        /// </summary>
        /// <param name="creator">On initial, this object is the creator member. On return from pi, it's a new member object</param>
        /// <returns></returns>
        public ActionResult LoadProfileImage(FamilyMemberModel creator)
        {
            using (repo = new FCoreRepository())
            {
                if (creator.Id != 0)
                    return PartialView("AddProfileImage", repo.GetFamilyMember(creator.Id));
                else
                {
                    var created = new FamilyMemberModel() { ProfileImagePath = (string)Session["filepath"] };
                    return PartialView("AddProfileImage", created);
                }
            }
        }

        [HttpPost]
        public ActionResult AddProfileImage(FamilyMemberModel creator, HttpPostedFileBase ProfileImagePath)
        {
            using (repo = new FCoreRepository())
            {
                if (Session["HBFB_file"] != null)
                {
                    Response.StatusCode = (int)HttpStatusCode.OK;
                    return Json(new { success = true });
                }

                var modelKeys = repo.GetModelKeys(ModelStateSet.ForProfileImage);
                foreach (var key in modelKeys) ModelState.Remove(key);

                if (ModelState.IsValid)
                {
                    if (ProfileImagePath.ContentType.Contains("image"))
                    {
                        Session["HBFB_file"] = ProfileImagePath;
                        Session["filepath"] = repo.GetFilePath(ProfileImagePath);
                        Session["filename"] = ProfileImagePath.FileName;

                        repo.UpdateMemberProfileImage(-1, ProfileImagePath, false);
                    }
                    else throw new FormatException("The target file is not type image.");

                    Response.StatusCode = (int)HttpStatusCode.OK;
                    return Json(new { success = true });
                }
                //else
                //{
                //    var errors = ModelState.SelectMany(state => state.Value.Errors.Select(error => error.ErrorMessage));
                //    throw new Exception($"Model is not valid. {errors}");
                //}

                Session["modelstate"] = ModelState.IsValid; // set to viewData with repo.SetModelState
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { success = false });
            }
        }

        [HttpGet]
        public ActionResult LoadPersonalInfo(FamilyMemberModel creator)
        {
            using (repo = new FCoreRepository())
            {
                Session["creatorId"] = creator.Id;

                ViewData["relenum"] = repo.GetChildRelationshipTypes();
                ViewData["genenum"] = repo.GetGenderTypes(); 

                return PartialView("AddPersonalInfo", new FamilyMemberModel());
            }
        }

        [HttpPost]
        public ActionResult AddPersonalInfo([Bind(Exclude = ("Permissions,Relatives"))]FamilyMemberModel postedMember, string Relationship)
        {
            using (repo = new FCoreRepository())
            {
                var modelKeys = repo.GetModelKeys(ModelStateSet.ForPersonalInfo);
                foreach (var key in modelKeys) ModelState.Remove(key);

                if (ModelState.IsValid)
                {
                    HttpPostedFileBase file = (HttpPostedFileBase)Session["HBFB_file"];
                    Session["personal_info"] = postedMember = repo.SetPersonalInfo(postedMember, repo.GetFilePath(file));
                    ViewData["cityenum"] = repo.GetCities();
                    return PartialView("AddContactInfo", new ContactInfoModel());
                }
                else
                {
                    ViewData = repo.SetModelState(ViewData, ModelState, ModelStateSet.ForPersonalInfo);

                    ViewData["relenum"] = repo.GetChildRelationshipTypes();
                    ViewData["genenum"] = repo.GetGenderTypes();
                    return PartialView(postedMember);
                }
            }
        }

        [HttpGet]
        public ActionResult LoadContactInfo(ContactInfoModel info)
        {
            using (repo = new FCoreRepository())
            {
                return PartialView("AddContactInfo", info);
            }
        }

        [HttpPost]
        public ActionResult AddContactInfo(ContactInfoModel info)
        {
            using (repo = new FCoreRepository())
            {
                if (ModelState.IsValid)
                {
                    var postedMember = (FamilyMemberModel)Session["personal_info"];
                    postedMember = repo.SetContactInfo(postedMember, info);
                    return PartialView("AddLifeStory", postedMember);
                }
                else
                {
                    return PartialView(info);
                }
            }
        }
    }
}