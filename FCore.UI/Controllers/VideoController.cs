using FCore.BL.Repositories;
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
            if (repo == null) repo = ViewBag.Repo;

            return View(repo.GetVideoLibraries());
        }

        public ActionResult GetLibrary(int id)
        {
            return View(repo.GetVideoLibrary(id));
        }
}