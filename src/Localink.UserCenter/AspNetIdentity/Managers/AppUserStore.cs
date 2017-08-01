using Localink.UserCenter.AspNetIdentity.Entitys;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Localink.UserCenter.AspNetIdentity.Managers
{
    public class AppUserStore : UserStore<AppUser, AppRole, long, AppUserLogin, AppUserRole, AppUserClaim>
    {
        public AppUserStore(AppIdentityDbContext context) : base(context)
        {
        }
    }
}