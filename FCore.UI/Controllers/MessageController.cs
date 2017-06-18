using FCore.BL.Repositories;
using FCore.Common.Interfaces;
using FCore.Common.Models.ChatGroups;
using FCore.Common.Models.Members;
using FCore.Common.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FCore.UI.Controllers
{
    [Authorize]
    public class MessageController : Controller
    {
        ICoreRepository repo { get; set; }

        public ActionResult ChatsPage()
        {
            using (repo = new FCoreRepository())
            {
                (Session["currentUser"] as UserModel).Member = repo.GetFamilyMember((Session["currentUser"] as UserModel).MemberId);

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
                (Session["currentUser"] as UserModel).Member = repo.GetFamilyMember((Session["currentUser"] as UserModel).MemberId);

                ChatGroupModel chat = repo.GetChatGroup(id);
                ICollection<FamilyMemberModel> chatMembers = repo.GetFamily(chat.FamilyId).FamilyMembers;
                ViewBag.ChatMembers = chatMembers;
                ViewData["cm"] = chatMembers;
                return View(repo.GetChatGroup(id));
            }
        }
    }
}