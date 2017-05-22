﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FCore.UI.Controllers
{
    public class AcountController : Controller
    {
        public UserManager<IdentityUser> userManager => HttpContext.GetOwinContext().Get<UserManager<IdentityUser>>();

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User model)
        {

        }
    }
}