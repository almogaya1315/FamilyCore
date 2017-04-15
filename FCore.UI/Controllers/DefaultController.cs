﻿using FCore.BL.Repositories;
using FCore.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FCore.UI.Controllers
{
    public class DefaultController : Controller
    {
        ICoreRepository repo { get; set; }

        public ActionResult Index()
        {
            repo = new FCoreRepository();
            return View();
        }

        public ActionResult GetMain()
        {
            return RedirectToAction("Main", "FamilyCore", repo);
        }
    }
}