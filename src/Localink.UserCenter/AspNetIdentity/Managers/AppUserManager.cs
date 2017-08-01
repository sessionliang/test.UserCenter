using Localink.UserCenter.AspNetIdentity.Entitys;
using Localink.UserCenter.AspNetIdentity.Validators;
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
    public class AppUserManager : UserManager<AppUser, long>
    {
        public AppUserManager(AppUserStore store) : base(store)
        {
        }

        public static AppUserManager Create(IdentityFactoryOptions<AppUserManager> options, IOwinContext context)
        {
            var dbContext = context.Get<AppIdentityDbContext>();
            return Create(dbContext);
        }

        public static AppUserManager Create(AppIdentityDbContext dbContext)
        {
            var store = new AppUserStore(dbContext);
            var userManager = new AppUserManager(store);

            //设置密码安全策略
            userManager.PasswordValidator = new CustomePasswordValidator()
            {
                RequiredLength = 6,
                RequireLowercase = true,
                RequireDigit = true,
                RequireNonLetterOrDigit = false,
                RequireUppercase = false
            };

            //设置用户安全策略
            userManager.UserValidator = new CustomeUserValidator(userManager)
            {
                AllowOnlyAlphanumericUserNames = true,//用户名只能包含数字，字母
                RequireUniqueEmail = true//邮箱唯一
            };

            return userManager;
        }
    }
}