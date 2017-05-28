using FCore.BL.Identity;
using FCore.BL.Repositories;
using FCore.Common.Enums;
using FCore.Common.Interfaces;
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

                    //var identityUser = await userRepo.GetUserAsync(model.UserName);

                    //if (identityUser != null)
                    //{
                    //    return RedirectToAction("Main", "FamilyCore", identityUser);
                    //}
                    //// todo.. signin manager
                }
                return View(model);
            }
        }

        public ActionResult RegisterPage()
        {
            return View();
        }

        public ActionResult LoadInitialInfo()
        {
            return PartialView("AddInitialInfo");
        }

        //[HttpPost]
        //public async Task<ActionResult> ValidatePassword(UserModel model)
        //{
        //    using (userRepo = new UserRepository(HttpContext))
        //    {
        //        var passValid = await userRepo.ValidatePassword(model.Password);
        //        Session["isValidPass"] = passValid.Succeeded;
        //        if (!passValid.Succeeded)
        //        {
        //            ModelState.Clear();
        //            //ModelState["Password"].Errors.Add(passValid.Errors.FirstOrDefault());
        //            ModelState.AddModelError("", passValid.Errors.FirstOrDefault());
        //        }

        //        return PartialView("AddInitialInfo", model);
        //    }
        //}

        [HttpPost]
        public async Task<ActionResult> AddInitialInfo([Bind(Exclude = "Claims,Logins,Roles")]UserModel model, HttpPostedFileBase ProfileImagePath)
        {
            using (userRepo = new UserRepository(HttpContext))
            {
                // todo.. able to enter funcion, only if username & password verified that not in use
                // todo.. create action that runs when username & password textbox text changed

                var modelKeys = ModelStateHelper.GetModelKeys(ModelStateSet.ForInitialInfo);
                foreach (var key in modelKeys) ModelState.Remove(key);

                ModelState.Remove("Member.ProfileImagePath"); // todo.. validation message in 'message_file' tag & new ajax http request 

                if (ModelState.IsValid)
                {
                    var identityUser = await userRepo.GetUserAsync(model.UserName);
                    if (identityUser == null)
                    {
                        var passValid = await userRepo.ValidatePassword(model.Password);
                        if (passValid.Succeeded) //if ((bool)Session["isValidPass"])
                        {
                            Session["username"] = model.UserName;
                            Session["password"] = model.Password;
                            //Session["HPFB_file"] = ProfileImagePath;
                            //Session["filepath"] = repo.GetFilePath(ProfileImagePath);
                            //Session["filename"] = ProfileImagePath.FileName;
                            return PartialView("AddPersonalInfo", new UserModel());
                        }
                        ModelState.AddModelError("invalid password", passValid.Errors.FirstOrDefault());
                    }
                    else
                    {
                        ModelState["UserName"].Errors.Add("Allready in use");
                    }
                }
            }
            return PartialView(model);
        }

        [HttpGet]
        public ActionResult LoadPersonalInfo()
        {
            return PartialView();
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