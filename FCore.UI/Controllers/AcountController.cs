﻿using FCore.BL.Identity;
using FCore.BL.Identity.Managers;
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

        // system enters here every 'verify' funcion in global.asax, when 'OwinStartup' is done & every action call from view => no 'using' needed!
        public AcountController(UserMemberManager userManager, LoginManager loginManager) 
        {
            userRepo = new UserRepository(userManager, loginManager); 
        }

        public ActionResult ClaimTest()
        {
            var res = userRepo.CliamTest();
            return null;
        }

        [HttpGet]
        public ActionResult LoginPage()
        {
            // ClaimTest();

            // todo.. varify past logged-in user & ask if to use OR which user if multiple
            Session.Clear();
            Session["validcolor"] = ModelStateHelper.ValidationMessageColor;
            return View();
        }

        [HttpPost] // used 'using (userRepo = new UserRepository(HttpContext))' before DI
        public async Task<ActionResult> LoginPage(UserModel model)
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

        [HttpPost]  // used 'using (userRepo = new UserRepository(HttpContext))' before DI
        public async Task<ActionResult> ValidateUsername([Bind(Exclude = "Claims,Logins,Roles")]UserModel model)
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

        [HttpPost]  // used 'using (userRepo = new UserRepository(HttpContext))' before DI
        public async Task<ActionResult> ValidatePassword(UserModel model)
        {
            var passValid = await userRepo.ValidatePassword(model.Password);
            Session["isValidPass"] = passValid.Succeeded;
            if (!passValid.Succeeded)
            {
                ModelState.Clear();
                var errors = ModelStateHelper.GetPasswordValidationErrors(passValid.Errors);
                for (int i = 0; i < errors.Count; i++)
                {
                    ModelState.AddModelError($"Pass{i}", errors.ElementAt(i));
                }
            }
            Session["temp_pass"] = model.Password;
            SetImageFileModelState();
            return PartialView("AddInitialInfo", model);
        }

        [HttpPost]  // used 'using (userRepo = new UserRepository(HttpContext))' before DI
        public ActionResult ValidateProfileImage(HttpPostedFileBase ProfileImagePath)
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

        [HttpPost]  // used 'using (userRepo = new UserRepository(HttpContext))' before DI
        public async Task<ActionResult> AddInitialInfo([Bind(Exclude = "Member,Claims,Logins,Roles")]UserModel model)
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

        [HttpPost]  // used 'using (userRepo = new UserRepository(HttpContext))' before DI
        public ActionResult AddPersonalInfo(UserModel model)
        {
            return PartialView("AddContactInfo", model);
        }

        // final step *** 

        [HttpPost]  // used 'using (userRepo = new UserRepository(HttpContext))' before DI
        public async Task<ActionResult> CreateUser(UserModel model)
        {
            var identityResult = await userRepo.CreateNewUserAsync(model);

            if (identityResult.Succeeded)
            {
                var userModel = await userRepo.GetUserAsync(model.UserName);
                return RedirectToAction("CreateSuccess", "Acount", userModel);
            }
            return PartialView(model);
        }
    }
}