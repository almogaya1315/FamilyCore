﻿using FCore.BL.Repositories;
using FCore.Common.Interfaces;
using FCore.Common.Models.Contacts;
using FCore.Common.Models.Members;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Caching;
using System.Web;
using System.Web.Mvc;

namespace FCore.UI.Controllers
{
    //[OutputCache(NoStore = true, Duration = 0)]
    public class ConfigController : Controller
    {
        ICoreRepository repo { get; set; }

        public ActionResult ConfigPage()
        {
            using (repo = new FCoreRepository())
            {
                return View(repo.GetFamilyMember(1));
            }
        }

        public ActionResult PersonalPage(FamilyMemberModel member)
        {
            using (repo = new FCoreRepository())
            {
                return View(repo.GetFamilyMember(member.Id));
            }
        }

        [HttpGet]
        public ActionResult EditAbout(int id)
        {
            using (repo = new FCoreRepository())
            {
                if (Request.IsAjaxRequest())
                {
                    return PartialView("EditAbout", repo.GetFamilyMember(id));
                }
                else return View("PersonalPage", repo.GetFamilyMember(id));
            }
        }

        [HttpPost]
        public ActionResult EditAbout([Bind(Exclude = "ContactInfo,Permissions,Relatives")] FamilyMemberModel member)
        {
            using (repo = new FCoreRepository())
            {
                if (ModelState.IsValid)
                {
                    if (repo.GetFamilyMember(member.Id).About != member.About)
                        repo.UpdateUserAbout(member.Id, member.About);
                    return PartialView("UserAbout", repo.GetFamilyMember(member.Id));
                }
                else return PartialView("EditAbout", repo.GetFamilyMember(member.Id));
            }
        }

        [HttpPost]
        public ActionResult EditProfileImage([Bind(Exclude = "ContactInfo,Permissions,Relatives")] FamilyMemberModel member, HttpPostedFileBase ProfileImagePath)
        {
            using (repo = new FCoreRepository())
            {
                if (ModelState.IsValid)
                {
                    if (ProfileImagePath.ContentType.Contains("image"))
                    {
                        if (repo.GetFilePath(ProfileImagePath) != member.ProfileImagePath)
                            repo.UpdateMemberProfileImage(member.Id, ProfileImagePath);
                    }
                    else { } // todo
                }
                return View("PersonalPage", repo.GetFamilyMember(member.Id));
            }
        }

        public ActionResult ContactDetails(FamilyMemberModel member)
        {
            using (repo = new FCoreRepository())
            {
                return View(repo.GetFamilyMember(member.Id));
            }
        }

        [HttpGet]
        public ActionResult EditDetails(int memberId, int contactInfoId)
        {
            using (repo = new FCoreRepository())
            {
                if (Request.IsAjaxRequest())
                {
                    return PartialView("EditDetails", repo.GetContactInfo(contactInfoId));
                }
                else return View("ContactDetails", repo.GetFamilyMember(memberId));
            }
        }

        [HttpPost]
        public ActionResult EditDetails([Bind(Exclude = "ContactBook")] ContactInfoModel postedInfo)
        {
            using (repo = new FCoreRepository())
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        repo.UpdateUserDetails(postedInfo);
                    }
                    catch (Exception e)
                    {
                        throw new Exception(e.Message);
                    }
                    return PartialView("UserDetails", repo.GetFamilyMember(postedInfo.MemberId));
                }
                return PartialView("EditDetails", repo.GetContactInfo(postedInfo.Id));
            }
        }

        public ActionResult RelativesDetails(FamilyMemberModel member)
        {
            using (repo = new FCoreRepository())
            {
                try
                {
                    member = repo.GetFamilyMember(member.Id);
                    foreach (RelativeModel relationship in member.Relatives)
                    {
                        relationship.Member = member;
                        relationship.Relative = repo.GetFamilyMember(relationship.RelativeId);
                    }
                }
                catch (Exception e)
                {
                    throw new Exception($"Unable to retrieve relatives data from db for member id #{member.Id}. {e.Message}");
                }
                return View("RelativesDetails", member);
            }
        }

        //[OutputCache(NoStore = true, Duration = 0)]
        public ActionResult SecurityPage(FamilyMemberModel member)
        {
            using (repo = new FCoreRepository())
            {
                member = repo.GetFamilyMember(member.Id);
                //if (member.Permissions.Admin)
                //{
                ViewData["memberId"] = member.Id;
                return View(member);
                //}
                //return View("ConfigPage", member); // todo -- need to return msg that user is not admin
            }
        }

        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult EditPermissions(PermissionsModel postedPerms, int memberId)
        {
            using (repo = new FCoreRepository())
            {
                //Response.Cache.SetCacheability(HttpCacheability.Server);

                if (MemoryCache.Default.Contains("permValue"))
                {
                    if (MemoryCache.Default.FirstOrDefault(cache => cache.Key == "permValue").Value != postedPerms)
                    {
                        if (!UpdateDatabaseAndCache(memberId, postedPerms)) return new HttpStatusCodeResult(500);
                    }
                }
                else
                {
                    if (!UpdateDatabaseAndCache(memberId, postedPerms)) return new HttpStatusCodeResult(500);
                }

                ViewData["memberId"] = memberId;
                return PartialView("EditPermissions", (PermissionsModel)MemoryCache.Default["permValue"]);
            }
        }

        private bool UpdateDatabaseAndCache(int memberId, PermissionsModel postedPerms)
        {
            if (ModelState.IsValid)
            {
                repo.UpdateUserPermissions(memberId, postedPerms);
            }
            else
            {
                var errors = ModelState.SelectMany(state => state.Value.Errors.Select(error => error.Exception));
                throw new Exception($"Model is not valid. {errors}");
            }

            try
            {
                FamilyMemberModel member = repo.GetFamilyMember(memberId);
                MemoryCache.Default.AddOrGetExisting("permValue", member.Permissions, DateTime.Now);
            }
            catch (Exception e)
            {
                throw new Exception($"Unable to update cache. {e.Message}");
            }

            return true;
        }
    }
}