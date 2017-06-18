using FCore.BL.Identity;
using FCore.BL.Identity.Managers;
using FCore.BL.Repositories;
using FCore.Common.Enums;
using FCore.Common.Interfaces;
using FCore.Common.Models.Contacts;
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
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
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
                Session["HPFB_file"] = null;
                Session["filepath"] = null;
                Session["filename"] = null;
            }
        }

        void SetImageFileModelState(bool nextStep)
        {
            if (Session["HPFB_file"] == null && nextStep)
            {
                ModelState.Remove("chooseImage");
                ModelState.AddModelError("requiredImage", "You must put in a profile picture");
            }
            else
            {
                ModelState.Remove("requiredImage");
                ModelState.AddModelError("chooseImage", "Click avatar to choose profile picture");
            }
        }

        void SetRelativeState()
        {
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
            }
            ViewData["genenum"] = ConstGenerator.GenderTypes;
        }

        void SetDropDownListData()
        {
            var ph = (ViewData["famenum"] as ICollection<SelectListItem>).FirstOrDefault(i => i.Value == "ph");
            (ViewData["famenum"] as ICollection<SelectListItem>).Remove(ph);
            ph = (ViewData["memenum"] as ICollection<SelectListItem>).FirstOrDefault(i => i.Value == "ph");
            (ViewData["memenum"] as ICollection<SelectListItem>).Remove(ph);
            (ViewData["memenum"] as ICollection<SelectListItem>).FirstOrDefault(i => i.Text == (Session["relative"] as FamilyMemberModel).FirstName).Selected = true;
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
            HttpCookie userCookie = HttpContext.Request.Cookies[ConstGenerator.UserIdentityCookieName];
            string userId = string.Empty;
            if (userCookie != null)
            {
                if (userCookie.Values.Count > 1)
                {
                    ICollection<UserModel> cookieUsers = new List<UserModel>();
                    foreach (string username in userCookie.Values)
                    {
                        var user = await userRepo.GetUserByUsrenameAsync(username);
                        user.Member = coreRepo.GetFamilyMember(user.MemberId);
                        cookieUsers.Add(user);
                    }
                    return View("ChooseUserPage", cookieUsers);
                }

                userId = userCookie.Values.GetValues(0).FirstOrDefault();
            }

            if (Session["logged-out"] == null)
            {
                Session.Clear();
                Session["validcolor"] = ModelStateHelper.ValidationMessageColor;

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
            }
            else Session["logged-out"] = null;

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> LoginPage(UserModel model)
        {
            if (ModelState.IsValid)
            {
                SignInStatus loginStatus = await userRepo.PasswordLoginAsync(model);

                switch (loginStatus)
                {
                    case SignInStatus.Success:
                        var identityUser = await userRepo.GetUserByUsrenameAsync(model.UserName);

                        HttpCookie userCookie = new HttpCookie(ConstGenerator.UserIdentityCookieName);
                        // for new logged-in user, when there are no cookie or values
                        if (Session["isCookie"] == null || !(bool)Session["isCookie"])
                        {
                            userCookie.Values.Add(identityUser.UserName, identityUser.Id.ToString());
                        }
                        else 
                        {
                            userCookie = HttpContext.Request.Cookies.Get(ConstGenerator.UserIdentityCookieName);
                            // if doesn't exists, add identity values to cookie, else return cookie values as is 
                            if (!userCookie.Values.AllKeys.Contains(identityUser.UserName))
                            {
                                userCookie.Values.Add(identityUser.UserName, identityUser.Id.ToString());
                            }
                        }
                        userCookie.Expires = DateTime.Now.AddYears(1);
                        HttpContext.Response.Cookies.Add(userCookie);

                        if (Session["currentUser"] == null || (Session["currentUser"] as UserModel).Id != identityUser.Id)
                        {
                            identityUser.Member = coreRepo.GetFamilyMember(identityUser.MemberId);
                            Session["currentUser"] = identityUser;
                        }

                        return RedirectToAction("Main", "FamilyCore", identityUser);
                    default:
                        ModelState.AddModelError("", "Invalid username or password");
                        break;
                }
            }
            return View(model);
        }

        public ActionResult Logout(UserModel model)
        {
            if (userRepo.LogOut(HttpContext))
            {
                ModelState.Remove("UserName");
                ModelState.Remove("Password");
                Session["logged-out"] = true;
                return RedirectToAction("LoginPage");
            }
            else throw new Exception("Unable to log-out.");
        }

        public ActionResult RegisterPage()
        {
            Session.Clear();
            Session["validcolor"] = ModelStateHelper.ValidationMessageColor;
            ModelState.AddModelError("chooseImage", "Click avatar to choose profile picture");
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

        [HttpPost]
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
            SetImageFileModelState(false);
            return PartialView("AddInitialInfo", model);
        }

        [HttpPost]
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
            SetImageFileModelState(false);
            return PartialView("AddInitialInfo", model);
        }

        [HttpPost]
        public ActionResult ValidateProfileImage(HttpPostedFileBase ProfileImagePath)
        {
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

        [HttpPost]
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
                            //Session["username"] = model.UserName;
                            //Session["password"] = model.Password;
                            Session["userModel"] = model;

                            model.Member = new FamilyMemberModel();
                            ViewData["relenum"] = ConstGenerator.RelTypes;
                            ViewData["genenum"] = ConstGenerator.GenderTypes;
                            ViewData["famenum"] = ConstGenerator.GetFamilySelectListItems(coreRepo.GetFamilies());
                            ViewData["memenum"] = ConstGenerator.GetMemberSelectListItems();
                            return PartialView("AddPersonalInfo", model.Member);
                        }
                        else SetImageFileModelState(true);
                    }
                    else return await ValidatePassword(model);
                }
                else return await ValidateUsername(model);
            }
            else SetImageFileModelState(true);
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
            var members = ConstGenerator.GetMemberSelectListItems(coreRepo.GetMembersDynamic(reqData.FamilyName, reqData.Text));
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
        public ActionResult LoadPersonalInfo()
        {
            ViewData["relenum"] = ConstGenerator.GetRelTypesSelectedItem((string)Session["rel"]);
            ViewData["genenum"] = ConstGenerator.GenderTypes;
            ViewData["famenum"] = ConstGenerator.GetFamilySelectListItems(new List<FamilyModel>() { coreRepo.GetFamily((string)Session["rel_fam"]) });
            ViewData["memenum"] = ConstGenerator.GetMemberSelectListItems(coreRepo.GetMembersDynamic((string)Session["rel_fam"]));

            SetDropDownListData();

            return PartialView("AddPersonalInfo", Session["member_pi"]);
        }

        [HttpPost]
        public ActionResult AddPersonalInfo([Bind(Exclude = "Permissions,Relatives")]FamilyMemberModel model, string Relationship)
        {
            SetRelativeState();

            var keys = ModelStateHelper.GetModelKeys(ModelStateSet.ForPersonalInfo);
            foreach (var key in keys) ModelState.Remove(key);
            if (ModelState.IsValid)
            {
                Session["member_pi"] = model;
                Session["rel"] = Relationship;
                ViewData["cityenum"] = ConstGenerator.Cities;
                return PartialView("AddContactInfo", new ContactInfoModel());
            }

            ViewData["relenum"] = ConstGenerator.RelTypes;
            ViewData["genenum"] = ConstGenerator.GenderTypes;
            return PartialView(model);
        }

        // back from ls
        [HttpGet]
        public ActionResult LoadContactInfo()
        {
            ViewData["cityenum"] = ConstGenerator.GetCitiesSelectedItem((Session["member_ci"] as ContactInfoModel).City);
            return PartialView("AddContactInfo", Session["member_ci"]);
        }

        [HttpPost]
        public ActionResult AddContactInfo(ContactInfoModel model)
        {
            var keys = ModelStateHelper.GetModelKeys(ModelStateSet.ForContactInfo);
            foreach (var key in keys) ModelState.Remove(key);

            if (ModelState.IsValid)
            {
                Session["member_ci"] = model;
                return PartialView("AddLifeStory", Session["member_pi"]);
            }

            return PartialView(model);
        }

        [HttpPost]
        public async Task<ActionResult> AddLifeStory([Bind(Exclude = ("Permissions,Relatives,ContactInfo"))]FamilyMemberModel model)
        {
            var keys = ModelStateHelper.GetModelKeys(ModelStateSet.ForLifeStory);
            foreach (var key in keys) ModelState.Remove(key);
            if (ModelState.IsValid)
            {
                (Session["userModel"] as UserModel).Member = (FamilyMemberModel)Session["member_pi"];
                (Session["userModel"] as UserModel).Member.About = model.About;
                (Session["userModel"] as UserModel).Member.ProfileImagePath = (string)Session["filepath"];
                (Session["userModel"] as UserModel).Member.ContactInfo = (ContactInfoModel)Session["member_ci"];
                var fullName = String.Format("{0} {1}", (Session["member_pi"] as FamilyMemberModel).FirstName, (Session["member_pi"] as FamilyMemberModel).LastName);
                (Session["userModel"] as UserModel).Member.ContactInfo.MemberName = (Session["userModel"] as UserModel).FullName = fullName;

                return await CreateUser((UserModel)Session["userModel"]);
            }
            return PartialView(model);
        }

        public async Task<ActionResult> CreateUser(UserModel model)
        {
            model.Member = coreRepo.CreateMember(model.Member, (Session["relative"] as FamilyMemberModel).Id, (string)Session["rel"]);
            model.MemberId = model.Member.Id;
            model.FamilyId = model.Member.FamilyId;
            var identityResult = await userRepo.CreateNewUserAsync(model);

            if (identityResult.Succeeded)
            {
                var userModel = await userRepo.GetUserByUsrenameAsync(model.UserName);
                userModel.Member = coreRepo.GetFamilyMember(model.Member.Id);
                userModel.Member = coreRepo.ConnectRelatives((Session["relative"] as FamilyMemberModel), userModel.Member);
                ViewData["relativeName"] = String.Format("{0}", (Session["relative"] as FamilyMemberModel).FirstName);
                return PartialView("CreateSuccess", userModel);
            }

            return PartialView("CreateFailure", model);
        }
    }
}