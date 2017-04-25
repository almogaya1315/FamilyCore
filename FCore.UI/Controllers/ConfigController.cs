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
                return View(repo.GetFamilyMember(1));
            }
        }

        public ActionResult PersonalPage(FamilyMemberModel member)
        {
            using (repo = new FCoreRepository())
            {
                return View(repo.GetFamilyMember(member.Id));
            }
        }

        [HttpGet]
        public ActionResult EditAbout(int id)
        {
            using (repo = new FCoreRepository())
            {
                return PartialView("EditAbout", repo.GetFamilyMember(id));
            }
        }

        [HttpPost]
        public ActionResult EditAbout(FamilyMemberModel member, string About)
        {
            using (repo = new FCoreRepository())
            {
                if (ModelState.IsValid)
                {

                }
                else return View(new { @from = "Get" });
                return RedirectToAction("PersonalPage", repo.GetFamilyMember(member.Id));
            }
        }
    }
}