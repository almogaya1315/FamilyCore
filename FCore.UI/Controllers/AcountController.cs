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
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(UserModel model)
        {
            using (userRepo = new UserRepository())
            {
                var identityResult = await userRepo.CreateNewUserAsync(model);

                if (identityResult.Succeeded)
                {
                    var userModel = await userRepo.GetUser(model.UserName);
                    return RedirectToAction("Main", "FamilyCore", userModel);
                }

                ModelState.AddModelError("", identityResult.Errors.FirstOrDefault());

                return View(model);
            }
        }
    }
}