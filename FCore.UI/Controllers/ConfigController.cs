using FCore.BL.Repositories;
using FCore.Common.Interfaces;
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
            return View();
        }

        [HttpGet]
        public ActionResult PersonalPage(int id)
        {
            using (repo = new FCoreRepository())
            {
                return View(repo.GetFamilyMember(id));
            }
        }

        [HttpPost]
        public ActionResult PersonalPage()
        {
            using (repo = new FCoreRepository())
            {
                if (ModelState.IsValid)
                {

                }
                return View();
            }
                
        }
    }
}