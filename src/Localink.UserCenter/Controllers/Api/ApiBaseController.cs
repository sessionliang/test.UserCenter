using Localink.UserCenter.AspNetIdentity.Managers;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Localink.UserCenter.Controllers.Api
{
    public abstract class ApiBaseController : ApiController
    {
        protected AppUserManager UserManager
        {
            get
            {
                return HttpContext.Current.Request.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }

        protected AppRoleManager RoleManager
        {
            get
            {
                return HttpContext.Current.Request.GetOwinContext().GetUserManager<AppRoleManager>();
            }
        }
    }
}