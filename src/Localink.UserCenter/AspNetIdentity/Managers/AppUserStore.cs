using Localink.UserCenter.AspNetIdentity.Entitys;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using Localink.UserCenter.Common;
using Microsoft.Owin;

namespace Localink.UserCenter.AspNetIdentity.Managers
{
    public class AppUserStore : UserStore<AppUser, AppRole, long, AppUserLogin, AppUserRole, AppUserClaim>
    {
        public AppUserStore(AppIdentityDbContext context) : base(context)
        {
        }

        public static AppUserStore Create(
                 IdentityFactoryOptions<AppUserStore> options,
                 IOwinContext context)
        {
            return Create(context.Get<AppIdentityDbContext>());
        }

        public static AppUserStore Create(AppIdentityDbContext dbContext)
        {
            return new AppUserStore(dbContext);
        }

        public async override Task<IList<Claim>> GetClaimsAsync(AppUser user)
        {
            var list = (await base.GetClaimsAsync(user));
            if (user.ForeName != null)
                list.Add(new Claim("forename", user.ForeName));
            if (user.SurName != null)
                list.Add(new Claim("surname", user.SurName));
            if (user.CountryCode != null)
                list.Add(new Claim("country_code", user.CountryCode));
            if (user.Email != null)
                list.Add(new Claim("email", user.Email));
            if (user.PhoneNumber != null)
                list.Add(new Claim("phone_number", user.PhoneNumber));
            if (user.Picture != null)
                list.Add(new Claim("picture", PathUtils.GetNetPath(user.Picture)));
            if (user.Address != null)
                list.Add(new Claim("address", user.Address));
            if (user.Gender != null)
                list.Add(new Claim("gender", user.Gender.ToString()));

            return list;
        }
    }
}