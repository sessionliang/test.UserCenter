using Localink.UserCenter.AspNetIdentity.Managers;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Localink.UserCenter.Controllers
{
    public abstract class BaseController : Controller
    {
        protected AppUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }

        protected AppUserStore UserStore
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppUserStore>();
            }
        }

        protected AppSignInManager SignInManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppSignInManager>();
            }
        }
    }
}