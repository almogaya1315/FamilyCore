using FCore.BL.Repositories;
using FCore.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FCore.UI.Controllers
{
    public class AlbumController : Controller
    {
        ICoreRepository repo { get; set; }

        public ActionResult AlbumsPage()
        {
            using (repo = new FCoreRepository())
            {
                return View(repo.GetAlbums());
            }
        }

        public ActionResult AlbumPage(int id)
        {
            using (repo = new FCoreRepository())
            {
                return View(repo.GetAlbum(id));
            }
        }
    }
}