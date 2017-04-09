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

            //ViewBag.LastJoinName = repo.GetLastMemberJoined();
            //ViewBag.VideoDesc = repo.GetMostViewedVideo();
            //ViewBag.LastImgDesc = repo.GetLastImage();

            return View(repo.GetFamilies());
        }
    }
}