﻿using FCore.BL.Repositories;
using FCore.Common.Interfaces;
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

        public ActionResult Main()
        {
            if (repo == null) repo = new FCoreRepository();

            ViewData["repo"] = repo;

            ViewBag.LastJoinName = repo.GetLastMemberJoined().FirstName;
            ViewBag.VideoDesc = repo.GetMostViewedVideo().Description;
            ViewBag.LastImgDesc = repo.GetLastImageUploaded().Description;

            return View(repo.GetFamilies());
        }
    }
}