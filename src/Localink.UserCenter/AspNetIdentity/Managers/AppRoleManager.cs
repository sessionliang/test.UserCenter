using Localink.UserCenter.AspNetIdentity.Entitys;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Localink.UserCenter.AspNetIdentity.Managers
{
    public class AppRoleManager : RoleManager<AppRole, long>, IDisposable
    {
        public AppRoleManager(AppRoleStore store) : base(store)
        {
        }

        public static AppRoleManager Create(
                 IdentityFactoryOptions<AppRoleManager> options,
                 IOwinContext context)
        {
            return Create(context.Get<AppIdentityDbContext>());
        }

        public static AppRoleManager Create(AppIdentityDbContext dbContext)
        {
            return new AppRoleManager(new
                    AppRoleStore(dbContext));
        }
    }
}