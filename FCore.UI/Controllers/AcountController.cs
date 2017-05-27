using FCore.BL.Identity;
using FCore.BL.Repositories;
using FCore.Common.Interfaces;
using FCore.Common.Models.Users;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FCore.UI.Controllers
{
    public class AcountController : Controller
    {
        public IUserRepository userRepo { get; set;}

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
                    var identityUser = await userRepo.GetUser(model.UserName);

                    if (identityUser != null)
                    {
                        return RedirectToAction("Main", "FamilyCore", identityUser);
                    }
                    // todo.. signin manager
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

        [HttpPost]
        public async Task<ActionResult> AddInitialInfo(UserModel model)
        {
            using (userRepo = new UserRepository(HttpContext))
            {
                // todo.. able to enter funcion, only if username & password verified that not in use
                // tofo.. create action that runs when exiting username & password textbox

                //if (ModelState.IsValid)
                //{
                    var identityResult = await userRepo.CreateNewUserAsync(model);

                    if (identityResult.Succeeded)
                    {
                        var userModel = await userRepo.GetUser(model.UserName);
                        return RedirectToAction("Main", "FamilyCore", userModel);
                    }
                    ModelState.AddModelError("", identityResult.Errors.FirstOrDefault());
                //}
            }
            return PartialView(model);
        }

        [HttpGet]
        public ActionResult LoadPersonalInfo()
        {
            return PartialView();
        }
    }
}