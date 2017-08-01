using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Localink.UserCenter.AspNetIdentity.Entitys
{
    /// <summary>
    /// 用户声明
    /// </summary>
    public class AppUserClaim : IdentityUserClaim<long>
    {
    }
}