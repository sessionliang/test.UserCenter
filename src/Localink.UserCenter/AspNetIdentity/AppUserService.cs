using Localink.UserCenter.AspNetIdentity;
using Localink.UserCenter.AspNetIdentity.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Localink.UserCenter.AspNetIdentity.Managers;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Services;
using Microsoft.AspNet.Identity.Owin;
using Localink.UserCenter.IdentityServer;

namespace Localink.UserCenter.AspNetIdentity
{
    public class AppUserService : AspNetIdentityUserService<AppUser, long>
    {
        public AppUserService(AppUserManager userManager) : base(userManager)
        {
        }
    }

    public static class CustomUserServiceExtensions
    {
        public static void ConfigureCustomUserService(this IdentityServerServiceFactory factory)
        {

            var appContext = AppIdentityDbContext.Create();

            factory.UserService = new Registration<IUserService, AppUserService>();
            factory.Register(new Registration<AppUserManager>(resolver => AppUserManager.Create(appContext)));
            factory.Register(new Registration<AppUserStore>());
            factory.Register(new Registration<AppRoleManager>(resolver => AppRoleManager.Create(appContext)));
            factory.Register(new Registration<AppRoleStore>());
            factory.Register(new Registration<AppIdentityDbContext>(resolver => appContext));

            //factory.AuthenticationSessionValidator = new Registration<IAuthenticationSessionValidator, CustomeAuthenticationSessionValidator>();
        }
    }
}