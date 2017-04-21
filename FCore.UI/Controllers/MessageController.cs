using FCore.BL.Repositories;
using FCore.Common.Interfaces;
using FCore.Common.Models.ChatGroups;
using FCore.Common.Models.Members;
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
                ICollection<ChatGroupModel> chatGroups = repo.GetChatGroups();
                IDictionary<int, string> familyIdName = new Dictionary<int, string>();
                foreach (var group in chatGroups)
                    familyIdName[group.FamilyId] = repo.GetFamily(group.FamilyId).Name;
                ViewBag.FamilyIdName = familyIdName;
                return View(repo.GetChatGroups());
            }
        }

        public ActionResult ChatPage(int id)
        {
            using (repo = new FCoreRepository())
            {
                ChatGroupModel chat = repo.GetChatGroup(id);
                ICollection<FamilyMemberModel> chatMembers = repo.GetFamily(chat.FamilyId).FamilyMembers;
                ViewBag.ChatMembers = chatMembers;
                return View(repo.GetChatGroup(id));
            }
        }
    }
}