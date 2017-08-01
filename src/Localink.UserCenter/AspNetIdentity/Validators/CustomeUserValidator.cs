using Localink.UserCenter.AspNetIdentity.Entitys;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace Localink.UserCenter.AspNetIdentity.Validators
{
    public class CustomeUserValidator : UserValidator<AppUser, long>
    {
        public CustomeUserValidator(UserManager<AppUser, long> manager) : base(manager)
        {

        }

        /// <summary>
        /// 自定义用户校验规则
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public override async Task<IdentityResult> ValidateAsync(AppUser user)
        {
            var result = await base.ValidateAsync(user);
            var errors = result.Errors.ToList();
            if (user.UserName.ToLower() == "admin")
            {
                errors.Add("admin is not allowed");
            }
            if (!user.Email.ToLower().EndsWith("@example.com"))
            {
                errors.Add("Only example.com email addresses are allowed");
                result = new IdentityResult(errors);
            }
            if (errors.Count > 0)
            {
                result = new IdentityResult(errors);
            }
            return result;
        }
    }
}