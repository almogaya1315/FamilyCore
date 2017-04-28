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
                if (Request.IsAjaxRequest())
                {
                    return PartialView("EditAbout", repo.GetFamilyMember(id));
                }
                else return View("PersonalPage", repo.GetFamilyMember(id));
            }
        }

        [HttpPost]
        public ActionResult EditAbout(FamilyMemberModel member)
        {
            using (repo = new FCoreRepository())
            {
                var error = ModelState.Values.Select(e => e.Errors);

                if (ModelState.IsValid) // && !String.IsNullOrWhiteSpace(member.About) 
                //need to pass model from 'EditAbout' partial's Ajax.BeginForm 
                //for ModelState to display 'About' property's error message 
                {
                    if (repo.GetFamilyMember(member.Id).About != member.About)
                        repo.UpdateUserAbout(member.Id, member.About);
                    return PartialView("UserAbout", repo.GetFamilyMember(member.Id));
                    //return RedirectToAction("PersonalPage", repo.GetFamilyMember(Id));
                }
                else return PartialView("EditAbout", repo.GetFamilyMember(member.Id));
            }
        }

        public ActionResult ContactDetails(FamilyMemberModel member)
        {
            using (repo = new FCoreRepository())
            {
                return View(repo.GetFamilyMember(member.Id));
            }
        }
    }
}