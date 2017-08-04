using Localink.UserCenter.AspNetIdentity;
using Localink.UserCenter.AspNetIdentity.Managers;
using Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Localink.UserCenter.App_Start
{
    public static class IdentityConfig
    {
        public static string IdsrvRootAddress = ConfigurationManager.AppSettings["IdentityServerRootAddress"];

        public static void Configure(IAppBuilder app)
        {
            app.CreatePerOwinContext(AppIdentityDbContext.Create);
            app.CreatePerOwinContext<AppUserManager>(AppUserManager.Create);
            app.CreatePerOwinContext<AppRoleManager>(AppRoleManager.Create);
        }
    }
}