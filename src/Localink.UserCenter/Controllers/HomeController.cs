﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Thinktecture.IdentityModel.Mvc;

namespace Localink.UserCenter.Controllers
{

    [HandleForbidden]
    public class HomeController : Controller
    {
        [Auth(Roles = "Administrators")]
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        [Authorize]
        public ActionResult Claims()
        {
            return View((User as ClaimsPrincipal).Claims);
        }

        public ActionResult Logout()
        {
            Request.GetOwinContext().Authentication.SignOut();
            return Redirect("/");
        }
    }
}
