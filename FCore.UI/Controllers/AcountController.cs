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
        public IUserRepository<UserModel> userRepo { get; set;}
        UserMemberManager userManager => HttpContext.GetOwinContext().Get<UserMemberManager>();

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
                await userRepo.CreateAsync(userManager, model);
                return null;
            }
        }
    }
}