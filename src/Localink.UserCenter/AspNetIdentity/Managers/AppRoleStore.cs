using Localink.UserCenter.AspNetIdentity.Entitys;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Localink.UserCenter.AspNetIdentity.Managers
{
    public class AppRoleStore : RoleStore<AppRole, long, AppUserRole>
    {
        public AppRoleStore(AppIdentityDbContext context) : base(context)
        {
        }
    }
}