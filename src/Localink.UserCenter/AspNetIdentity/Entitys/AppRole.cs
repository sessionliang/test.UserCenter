using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Localink.UserCenter.AspNetIdentity.Entitys
{
    /// <summary>
    /// 角色
    /// </summary>
    public class AppRole : IdentityRole<long, AppUserRole>
    {
        public AppRole() { }

        public AppRole(string roleName)
        {
            Name = roleName;
        }
    }
}