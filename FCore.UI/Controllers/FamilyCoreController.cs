using FCore.BL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FCore.UI.Controllers
{
    public class FamilyCoreController : Controller
    {
        FCoreRepository repo { get; set; }

        public ActionResult Main()
        {
            if (repo == null) repo = new FCoreRepository();

            return View(repo.);
        }
    }
}