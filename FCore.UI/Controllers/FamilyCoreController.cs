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
    [Authorize]
    public class FamilyCoreController : Controller
    {
        ICoreRepository coreRepo { get; set; }

        public ActionResult Main(UserModel userModel)
        {
            using (coreRepo = new FCoreRepository())
            {
                userModel.Member = coreRepo.GetFamilyMember(userModel.MemberId);

                ViewBag.LastJoinName = coreRepo.GetLastMemberJoined().FirstName;
                ViewBag.VideoDesc = coreRepo.GetMostViewedVideo().Description;
                ViewBag.LastImgDesc = coreRepo.GetLastImageUploaded().Description;
                ViewBag.FirstFamily = "Matsliah"; // todo..

                return View(userModel);
            }
        }
    }
}