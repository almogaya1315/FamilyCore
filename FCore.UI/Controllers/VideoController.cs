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
    public class VideoController : Controller
    {
        ICoreRepository repo { get; set; }
        public ActionResult LibrariesPage()
        {
            using (repo = new FCoreRepository())
            {
                (Session["currentUser"] as UserModel).Member = repo.GetFamilyMember((Session["currentUser"] as UserModel).MemberId);

                return View(repo.GetVideoLibraries());
            }
        }

        public ActionResult LibraryPage(int id)
        {
            using (repo = new FCoreRepository())
            {
                (Session["currentUser"] as UserModel).Member = repo.GetFamilyMember((Session["currentUser"] as UserModel).MemberId);

                return View(repo.GetVideoLibrary(id)); 
            }
        }
    }
}