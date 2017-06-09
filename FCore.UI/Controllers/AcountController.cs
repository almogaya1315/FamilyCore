using FCore.BL.Identity;
using FCore.BL.Identity.Managers;
using FCore.BL.Repositories;
using FCore.Common.Enums;
using FCore.Common.Interfaces;
using FCore.Common.Models.Families;
using FCore.Common.Models.Members;
using FCore.Common.Models.Users;
using FCore.Common.Support;
using FCore.Common.Utils;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace FCore.UI.Controllers
{


    [AllowAnonymous]
    public class AcountController : Controller
    {
        #region private data
        FCoreRepository coreRepo { get; set; }
        IUserRepository userRepo { get; set; }

        void CheckIfImageAndUpload(HttpPostedFileBase ProfileImagePath)
        {
            if (ProfileImagePath.ContentType.Contains("image"))
            {
                Session["HPFB_file"] = ProfileImagePath;
                Session["filepath"] = InputHelper.GetFilePath(ProfileImagePath);
                Session["filename"] = ProfileImagePath.FileName;

                InputHelper.UploadProfileImage(ProfileImagePath);
            }
            else
            {
                Session["valid_profileimage"] = false;
                Session["profileimage_modelstate"] = false;

                Session["HPFB_file"] = null;
                Session["filepath"] = null;
                Session["filename"] = null;
            }
        }

        void SetImageFileModelState()
        {
            if (Session["imgClicked"] != null)
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
        }

        bool SetRelativeState()
        {
            var relativePicked = false;
            if (Session["relative"] == null)
            {
                if (Session["rel_fam"] == null)
                {
                    ModelState.AddModelError("fam_required", "Pick relative family");
                    ViewData["famenum"] = ConstGenerator.GetFamilySelectListItems(coreRepo.GetFamilies());
                    ViewData["memenum"] = ConstGenerator.GetMemberSelectListItems();
                }
                else
                {
                    ModelState.Remove("fam_required");
                    ModelState.AddModelError("mem_required", "Pick relative name");
                    ViewData["famenum"] = ConstGenerator.GetFamilySelectListItems(new List<FamilyModel>()
                        { coreRepo.GetFamily((string)Session["rel_fam"]) });
                    ViewData["memenum"] = ConstGenerator.GetMemberSelectListItems();
                }
            }
            else
            {
                ModelState.Remove("mem_required");
                ViewData["famenum"] = ConstGenerator.GetFamilySelectListItems(new List<FamilyModel>()
                    { coreRepo.GetFamily((string)Session["rel_fam"]) });
                ViewData["memenum"] = ConstGenerator.GetMemberSelectListItems(new List<FamilyMemberModel>()
                    { (FamilyMemberModel)Session["relative"] });
                relativePicked = true;
            }
            return relativePicked;
        }
        #endregion

        // system enters here every 'verify' funcion in global.asax, 
        // when 'OwinStartup' is done & every action call from view => no 'using' needed!
        public AcountController(UserMemberManager userManager, LoginManager loginManager)
        {
            coreRepo = new FCoreRepository();
            userRepo = new UserRepository(userManager, loginManager);
        }

        [HttpGet]
        public async Task<ActionResult> LoginPage()
        {
            Session.Clear();
            Session["validcolor"] = ModelStateHelper.ValidationMessageColor;

            var cookie = HttpContext.Request.Cookies["userCookie"];
            string userId = string.Empty;
            if (cookie != null) userId = cookie.Value;

            if (!string.IsNullOrWhiteSpace(userId))
            {
                var user = await userRepo.GetUserByIdAsync(userId);
                if (user != null)
                {
                    Session["isCookie"] = true;
                    return await LoginPage(user);
                }
                else throw new Exception(); // todo..
            }
            Session["isCookie"] = false;
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
                        var identityUser = await userRepo.GetUserByUsrenameAsync(model.UserName);

                        // for new logged-in user
                        if (Session["isCookie"] == null || !(bool)Session["isCookie"])
                        {
                            HttpCookie userCookie = new HttpCookie("userCookie", identityUser.Id);
                            userCookie.Expires.AddYears(1);
                            HttpContext.Response.Cookies.Add(userCookie);
                        }

                        if (Session["cureentUser"] == null)
                        {
                            identityUser.Member = coreRepo.GetFamilyMember(identityUser.MemberId);
                            Session["cureentUser"] = identityUser;
                        }

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
            if (Session["isValidPass"] != null && (bool)Session["isValidPass"] || Session["temp_pass"] != null)
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
            else if (model.Member == null)
            {
                model.Member = new FamilyMemberModel();
                if (ModelState["Member.ProfileImagePath"] == null)
                    ModelState.Add("Member.ProfileImagePath", new ModelState());
                ModelState["Member.ProfileImagePath"].Errors.Clear();
                ModelState["Member.ProfileImagePath"].Errors.Add("The target file is not type image");
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
                var identityUser = await userRepo.GetUserByUsrenameAsync(model.UserName);
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
                var errors = ModelStateHelper.GetPasswordValidationErrors(passValid.Errors.FirstOrDefault());
                for (int i = 0; i < errors.Count; i++)
                    ModelState.AddModelError($"Pass{i}", errors.ElementAt(i));
            }
            Session["temp_pass"] = model.Password;
            SetImageFileModelState();
            return PartialView("AddInitialInfo", model);
        }

        [HttpPost]  // used 'using (userRepo = new UserRepository(HttpContext))' before DI
        public ActionResult ValidateProfileImage(HttpPostedFileBase ProfileImagePath)
        {
            Session["imgClicked"] = true;

            if (Session["HPFB_file"] != null)
            {
                if (Session["HPFB_file"] != ProfileImagePath)
                {
                    CheckIfImageAndUpload(ProfileImagePath);
                }
            }
            else CheckIfImageAndUpload(ProfileImagePath);

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

                            model.Member = new FamilyMemberModel();
                            ViewData["genenum"] = ConstGenerator.GenderTypes;
                            ViewData["famenum"] = ConstGenerator.GetFamilySelectListItems(coreRepo.GetFamilies());
                            ViewData["memenum"] = ConstGenerator.GetMemberSelectListItems(); 
                            return PartialView("AddPersonalInfo", model);
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

        [HttpPost]
        public ActionResult LoadFamiliesDynamic(TextBox box)
        {
            var families = ConstGenerator.GetFamilySelectListItems(coreRepo.GetFamiliesDynamic(box.Text));
            Response.StatusCode = (int)HttpStatusCode.OK;
            return Json(new { success = true, Families = families }); 
        }

        [HttpPost]
        public ActionResult LoadMembersDynamic(DynamicMemberRequestData reqData)
        {
            var members = ConstGenerator.GetMemberSelectListItems(coreRepo.GetMembersDynamic(reqData. FamilyName, reqData.Text));
            Session["rel_fam"] = reqData.FamilyName;
            Response.StatusCode = (int)HttpStatusCode.OK;
            return Json(new { success = true, Members = members });
        }
        [HttpPost]
        public ActionResult AddRelative(FamilyMemberModel relative)
        {
            var relatives = coreRepo.GetFamilyMember((string)Session["rel_fam"], relative.FirstName); 
            if (relatives.Count > 1)
            {
                // todo.. in case there is more then one member in a family that has the choosen name, 
                //        the user will have to choose exactly which from a pop-up list
            }
            else Session["relative"] = relatives.FirstOrDefault();

            Response.StatusCode = (int)HttpStatusCode.OK;
            return Json(new { success = true });
        }

        // back from ci
        [HttpGet]
        public ActionResult LoadPersonalInfo(UserModel model)
        {
            model.Member = new FamilyMemberModel();

            ViewData["genenum"] = ConstGenerator.GenderTypes;
            ViewData["famenum"] = ConstGenerator.GetFamilySelectListItems(new List<FamilyModel>()
                { coreRepo.GetFamily((string)Session["rel_fam"]) });
            ViewData["memenum"] = ConstGenerator.GetMemberSelectListItems(new List<FamilyMemberModel>()
                { (FamilyMemberModel)Session["relative"] });
            return PartialView("AddPersonalInfo", model);
        }

        [HttpPost]  // used 'using (userRepo = new UserRepository(HttpContext))' before DI
        public ActionResult AddPersonalInfo(UserModel model)
        {
            var relativePicked = SetRelativeState();

            var keys = ModelStateHelper.GetModelKeys(ModelStateSet.ForPersonalInfo);
            foreach (var key in keys) ModelState.Remove(key);
            if (ModelState.IsValid && relativePicked)
            {
                Session["member_pi"] = model.Member;
                return PartialView("AddContactInfo", model);
            }

            return PartialView(model);
        }

        // final step *** 

        [HttpPost]  // used 'using (userRepo = new UserRepository(HttpContext))' before DI
        public async Task<ActionResult> CreateUser(UserModel model)
        {
            var identityResult = await userRepo.CreateNewUserAsync(model);

            if (identityResult.Succeeded)
            {
                var userModel = await userRepo.GetUserByUsrenameAsync(model.UserName);
                return RedirectToAction("CreateSuccess", "Acount", userModel);
            }
            return PartialView(model);
        }
    }
}