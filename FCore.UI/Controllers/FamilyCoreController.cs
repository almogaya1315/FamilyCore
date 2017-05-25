using FCore.BL.Repositories;
using FCore.Common.Interfaces;
using FCore.Common.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FCore.UI.Controllers
{
    public class FamilyCoreController : Controller
    {
        ICoreRepository repo { get; set; }

        public ActionResult Main(UserModel model)
        {
            using (repo = new FCoreRepository())
            {
                ViewBag.LastJoinName = repo.GetLastMemberJoined().FirstName;
                ViewBag.VideoDesc = repo.GetMostViewedVideo().Description;
                ViewBag.LastImgDesc = repo.GetLastImageUploaded().Description;
                //ViewBag.FirstFamily = 

                return View(repo.GetFamilies());
            }
        }
    }
}