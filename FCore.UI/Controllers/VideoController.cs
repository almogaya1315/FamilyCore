﻿using FCore.BL.Repositories;
using FCore.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FCore.UI.Controllers
{
    public class VideoController : Controller
    {
        ICoreRepository repo { get; set; }
        public ActionResult MainPage()
        {
            repo = new FCoreRepository();

            return View(repo.GetVideoLibraries());
        }

        public ActionResult LibraryPage(int id)
        {
            return View(repo.GetVideoLibrary(id));
        }

        public ActionResult VideoPage(int id)
        {
            return View();
        }
    }
}