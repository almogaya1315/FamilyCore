using FCore.BL.Repositories;
using FCore.Common.Interfaces;
using FCore.Common.Models.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FCore.UI.Controllers
{
    public class ConfigController : Controller
    {
        ICoreRepository repo { get; set; }

        public ActionResult ConfigPage()
        {
            using (repo = new FCoreRepository())
            {
                FamilyMemberModel member = repo.GetFamilyMember(1);
                return View(member);
            }
        }

        public ActionResult PersonalPage(FamilyMemberModel member)
        {
            using (repo = new FCoreRepository())
            {
                return View(member);
            }
        }

        [HttpGet]
        public PartialViewResult EditAbout(FamilyMemberModel member)
        {
            using (repo = new FCoreRepository())
            {
                return PartialView(member);
            }
        }

        [HttpPost]
        public ActionResult EditAbout(FamilyMemberModel member, string about)
        {
            using (repo = new FCoreRepository())
            {
                if (ModelState.IsValid)
                {

                }
                else return View(new { @from = "Get" });
                return RedirectToAction("PersonalPage", member);
            }
        }
    }
}