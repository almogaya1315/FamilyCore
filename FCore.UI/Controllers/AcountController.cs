using FCore.BL.Identity;
using FCore.BL.Repositories;
using FCore.Common.Enums;
using FCore.Common.Interfaces;
using FCore.Common.Models.Members;
using FCore.Common.Models.Users;
using FCore.Common.Utils;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FCore.UI.Controllers
{
    public class AcountController : Controller
    {
        public IUserRepository userRepo { get; set; }

        [HttpGet]
        public ActionResult LoginPage()
        {
            // todo.. varify past logged-in user & ask if to use OR which user if multiple
            Session.Clear();
            Session["validcolor"] = ModelStateHelper.ValidationMessageColor;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> LoginPage(UserModel model)
        {
            using (userRepo = new UserRepository(HttpContext))
            {
                if (ModelState.IsValid)
                {
                    SignInStatus loginStatus = await userRepo.PasswordLoginAsync(model);

                    switch (loginStatus)
                    {
                        case SignInStatus.Success:
                            var identityUser = await userRepo.GetUserAsync(model.UserName);
                            return RedirectToAction("Main", "FamilyCore", identityUser);
                        default:
                            ModelState.AddModelError("", "Invalid username or password");
                            break;
                    }
                }
                return View(model);
            }
        }

        public ActionResult RegisterPage()
        {
            Session.Clear();
            Session["validcolor"] = ModelStateHelper.ValidationMessageColor;
            return View();
        }

        public ActionResult LoadInitialInfo(UserModel model)
        {
            if (Session["isValidUsername"] != null && (bool)Session["isValidUsername"])
            {
                model.UserName = (string)Session["temp_username"];
                ModelState.Remove("UserName");
            }
            if (Session["isValidPass"] != null && (bool)Session["isValidPass"])
            {
                model.Password = (string)Session["temp_pass"];
                ModelState.Remove("Password");
            }
            if (Session["HPFB_file"] != null)
            {
                if (model.Member == null) model.Member = new FamilyMemberModel();
                model.Member.ProfileImagePath = (string)Session["filepath"];
                ModelState.Remove("Member.ProfileImagePath");
            }
            return PartialView("AddInitialInfo", model);
        }

        [HttpPost]
        public async Task<ActionResult> ValidateUsername([Bind(Exclude = "Claims,Logins,Roles")]UserModel model)
        {
            using (userRepo = new UserRepository(HttpContext))
            {
                var modelKeys = ModelStateHelper.GetModelKeys(ModelStateSet.ForUsernameValidation);
                foreach (var key in modelKeys) ModelState.Remove(key);

                if (ModelState.IsValid)
                {
                    var identityUser = await userRepo.GetUserAsync(model.UserName);
                    if (identityUser == null)
                    {
                        Session["isValidUsername"] = true;
                        Session["temp_username"] = model.UserName;
                    }
                    else
                    {
                        Session["isValidUsername"] = false;
                        ModelState["UserName"].Errors.Add("Allready in use");
                    }
                }
                SetImageFileModelState();
                return PartialView("AddInitialInfo", model);
            }
        }

        [HttpPost]
        public async Task<ActionResult> ValidatePassword(UserModel model)
        {
            using (userRepo = new UserRepository(HttpContext))
            {
                var passValid = await userRepo.ValidatePassword(model.Password);
                Session["isValidPass"] = passValid.Succeeded;
                if (!passValid.Succeeded)
                {
                    ModelState.Clear();
                    ModelState.AddModelError("PassSpecificReq", passValid.Errors.FirstOrDefault());
                }
                Session["temp_pass"] = model.Password;
                SetImageFileModelState();
                return PartialView("AddInitialInfo", model);
            }
        }

        [HttpPost]
        public ActionResult ValidateProfileImage(HttpPostedFileBase ProfileImagePath)
        {
            using (userRepo = new UserRepository(HttpContext))
            {
                if (Session["HPFB_file"] != null)
                {
                    Response.StatusCode = (int)HttpStatusCode.OK;
                    return Json(new { success = true });
                }

                if (ProfileImagePath.ContentType.Contains("image"))
                {
                    Session["HPFB_file"] = ProfileImagePath;
                    Session["filepath"] = InputHelper.GetFilePath(ProfileImagePath);
                    Session["filename"] = ProfileImagePath.FileName;

                    InputHelper.UploadProfileImage(ProfileImagePath);
                }
                else
                {
                    ModelState["Member.ProfileImagePath"].Errors.Clear();
                    ModelState["Member.ProfileImagePath"].Errors.Add("The target file is not type image");
                    Session["valid_profileimage"] = false;

                    //throw new FormatException("The target file is not type image.");
                }

                Response.StatusCode = (int)HttpStatusCode.OK;
                return Json(new { success = true });
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddInitialInfo([Bind(Exclude = "Member,Claims,Logins,Roles")]UserModel model)
        {
            using (userRepo = new UserRepository(HttpContext))
            {
                var modelKeys = ModelStateHelper.GetModelKeys(ModelStateSet.ForInitialInfo);
                foreach (var key in modelKeys) ModelState.Remove(key);

                if (ModelState.IsValid)
                {
                    if ((bool)Session["isValidUsername"])
                    {
                        if ((bool)Session["isValidPass"])
                        {
                            if (Session["HPFB_file"] != null)
                            {
                                Session["username"] = model.UserName;
                                Session["password"] = model.Password;

                                ViewData["relenum"] = ConstGenerator.ChildRelTypes;
                                ViewData["genenum"] = ConstGenerator.GenderTypes;
                                return PartialView("AddPersonalInfo", new UserModel());
                            }
                            else SetImageFileModelState();
                        }
                        else return await ValidatePassword(model);
                    }
                    else return await ValidateUsername(model);
                }
                else SetImageFileModelState();
            }
            return PartialView(model);
        }

        void SetImageFileModelState()
        {
            if (Session["HPFB_file"] == null)
            {
                Session["profileimage_modelstate"] = false;
                ModelState.Remove("Member.ProfileImagePath");
                ModelState.AddModelError("Member.ProfileImagePath", "You must put in a profile picture");
            }
            else
            {
                Session["profileimage_modelstate"] = null;
                ModelState.Remove("Member.ProfileImagePath");
            }
        }

        [HttpGet]
        public ActionResult LoadPersonalInfo()
        {
            ViewData["relenum"] = ConstGenerator.ChildRelTypes;
            ViewData["genenum"] = ConstGenerator.GenderTypes;
            return PartialView("AddPersonalInfo");
        }

        [HttpPost]
        public ActionResult AddPersonalInfo(UserModel model)
        {
            using (userRepo = new UserRepository(HttpContext))
            {
                return PartialView("AddContactInfo", model);
            }
        }

        // *** 

        [HttpPost]
        public async Task<ActionResult> CreateUser(UserModel model)
        {
            using (userRepo = new UserRepository(HttpContext))
            {
                var identityResult = await userRepo.CreateNewUserAsync(model); // for final step ***

                if (identityResult.Succeeded) // for final step ***
                {
                    var userModel = await userRepo.GetUserAsync(model.UserName);
                    return RedirectToAction("CreateSuccess", "Acount", userModel);
                }
                return PartialView(model);
            }
        }
    }
}