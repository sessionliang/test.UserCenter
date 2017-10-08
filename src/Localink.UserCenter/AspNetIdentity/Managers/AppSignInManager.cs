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
            return Create(context.Get<AppUserManager>(), context.Authentication);
        }

        public static AppSignInManager Create(UserManager<AppUser, long> userManager, IAuthenticationManager authenticationManager)
        {
            var signInManager = HttpContext.Current.GetOwinContext().Get<AppSignInManager>();
            if (signInManager == null)
            {
                signInManager = new AppSignInManager(userManager, authenticationManager);
            }
            return signInManager;
        }
    }
}