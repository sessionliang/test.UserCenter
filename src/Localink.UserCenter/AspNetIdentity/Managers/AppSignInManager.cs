using Localink.UserCenter.AspNetIdentity.Entitys;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin;

namespace Localink.UserCenter.AspNetIdentity.Managers
{
    public class AppSignInManager : SignInManager<AppUser, long>
    {
        public AppSignInManager(UserManager<AppUser, long> userManager, IAuthenticationManager authenticationManager) : base(userManager, authenticationManager)
        {
        }

        public static AppSignInManager Create(IdentityFactoryOptions<AppSignInManager> options, IOwinContext context)
        {
            var signInManager = new AppSignInManager(context.Get<AppUserManager>(), context.Authentication);
            return signInManager;
        }

        public static AppSignInManager Create(UserManager<AppUser, long> userManager, IAuthenticationManager authenticationManager)
        {
            var signInManager = new AppSignInManager(userManager, authenticationManager);
            return signInManager;
        }
    }
}