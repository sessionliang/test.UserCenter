using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Thinktecture.IdentityModel.Mvc;

namespace Localink.UserCenter.Controllers
{
    [HandleForbidden]
    [Auth(Roles = "Administrators")]
    public class UserController : BaseController
    {
        // GET: User
        public ActionResult Index()
        {
            return View(UserManager.Users);
        }
    }
}