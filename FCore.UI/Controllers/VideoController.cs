using FCore.BL.Repositories;
using FCore.Common.Interfaces;
using FCore.Common.Models.Users;
using FCore.Common.Models.Videos;
using FCore.Common.Utils;
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

                return View("LibraryPage", repo.GetVideoLibrary(id));
            }
        }

        [HttpPost]
        public ActionResult AddVideo(HttpPostedFileBase videoFile, int libId)
        {
            if (videoFile == null) throw new NullReferenceException();
            if (videoFile.ContentType.Contains("video"))
            {
                if (ModelState["videoType"] != null) ModelState.Remove("videoType");
                //InputHelper.UploadVideo(videoFile);
                //repo.SaveVideo(InputHelper.GetFilePath(videoFile), libId);
            }
            else
            {
                ModelState.AddModelError("videoType", "The target file is not type video");
                return View("LibraryPage", repo.GetVideoLibrary(libId));
            }

            return View();
        }

        [HttpGet]
        public ActionResult EditVideoDesc(VideoModel video)
        {
            if (video == null) throw new NullReferenceException();
            return PartialView(video);
        }

        [HttpPost]
        public ActionResult EditVideoDesc(int id, string newDesc)
        {
            using (repo = new FCoreRepository())
            {
                if (id == default(int)) throw new Exception("Parameter 'id' was not passed to server correctly.");
                if (newDesc == string.Empty) return PartialView();
                var video = repo.UpdateVideoDesc(id, newDesc);
                return PartialView("VideoDesc", video);
            }
        }

        public ActionResult DeleteVideo(VideoModel video)
        {
            using (repo = new FCoreRepository())
            {
                int libraryId = default(int);
                repo.DeleteVideo(video, out libraryId);
                return LibraryPage(libraryId);
                //return RedirectToAction("LibraryPage", libraryId);
            }
        }
    }
}