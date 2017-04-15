using FCore.BL.Repositories;
using FCore.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FCore.UI.Controllers
{
    public class MessageController : Controller
    {
        ICoreRepository repo { get; set; }

        public ActionResult MessageChatsPage()
        {
            using (repo = new FCoreRepository())
            {
                return View(repo.GetChatGroups());
            }
        }

        public ActionResult ChatPage(int id)
        {
            using (repo = new FCoreRepository())
            {
                return View(repo.GetChatGroup(id));
            }
        }
    }
}