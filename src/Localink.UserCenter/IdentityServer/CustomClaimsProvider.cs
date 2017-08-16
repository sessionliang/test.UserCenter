using IdentityServer3.Core.Services.Default;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IdentityServer3.Core.Services;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Validation;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using IdentityServer3.Core;
using Localink.UserCenter.AspNetIdentity.Managers;
using Microsoft.AspNet.Identity;
using Localink.UserCenter.Common;

namespace Localink.UserCenter.IdentityServer
{
    public class CustomClaimsProvider : DefaultClaimsProvider
    {
        public CustomClaimsProvider(IUserService users) : base(users)
        {

        }

        //public async override Task<IEnumerable<Claim>> GetAccessTokenClaimsAsync(ClaimsPrincipal subject, Client client, IEnumerable<Scope> scopes, ValidatedRequest request)
        //{
        //    var list = (await base.GetAccessTokenClaimsAsync(subject, client, scopes, request)).ToList();
        //    await AddCustomClaimsAsync(list, subject);
        //    return list;
        //}

        //public async override Task<IEnumerable<Claim>> GetIdentityTokenClaimsAsync(ClaimsPrincipal subject, Client client, IEnumerable<Scope> scopes, bool includeAllIdentityClaims, ValidatedRequest request)
        //{
        //    var list = (await base.GetIdentityTokenClaimsAsync(subject, client, scopes, includeAllIdentityClaims, request)).ToList();
        //    await AddCustomClaimsAsync(list, subject);
        //    return list;
        //}

        //protected override IEnumerable<Claim> GetStandardSubjectClaims(ClaimsPrincipal subject)
        //{
        //    var list = (base.GetStandardSubjectClaims(subject)).ToList();
        //    AddCustomClaims(list, subject);
        //    return list;
        //}


        protected override IEnumerable<Claim> GetOptionalClaims(ClaimsPrincipal subject)
        {
            var list = (base.GetOptionalClaims(subject)).ToList();
            AddCustomClaims(list, subject);
            return list;
        }

        private async Task AddCustomClaimsAsync(List<Claim> list, ClaimsPrincipal subject)
        {
            var userManager = HttpContext.Current.Request.GetOwinContext().GetUserManager<AppUserManager>();
            var id = Convert.ToInt64(subject.Claims.First(c => c.Type == IdentityServer3.Core.Constants.ClaimTypes.Subject)?.Value);
            var userDb = await userManager.FindByIdAsync(id);
            if (!list.Any(c => c.Type == "forename") && userDb.ForeName != null)
                list.Add(new Claim("forename", userDb.ForeName));

            if (!list.Any(c => c.Type == "surname") && userDb.SurName != null)
                list.Add(new Claim("surname", userDb.SurName));

            if (!list.Any(c => c.Type == "country_code") && userDb.CountryCode != null)
                list.Add(new Claim("country_code", userDb.CountryCode));

            if (!list.Any(c => c.Type == "email") && userDb.Email != null)
                list.Add(new Claim("email", userDb.Email));


            if (!list.Any(c => c.Type == "phone_number") && userDb.PhoneNumber != null)
                list.Add(new Claim("phone_number", userDb.PhoneNumber));

            if (!list.Any(c => c.Type == "picture") && userDb.Picture != null)
                list.Add(new Claim("picture", PathUtils.GetNetPath(userDb.Picture)));

            if (!list.Any(c => c.Type == "address") && userDb.Address != null)
                list.Add(new Claim("address", userDb.Address));


            if (!list.Any(c => c.Type == "gender") && userDb.Gender != null)
                list.Add(new Claim("gender", userDb.Gender.ToString()));

        }

        private void AddCustomClaims(List<Claim> list, ClaimsPrincipal subject)
        {
            var userManager = HttpContext.Current.Request.GetOwinContext().GetUserManager<AppUserManager>();
            var id = Convert.ToInt64(subject.Claims.First(c => c.Type == IdentityServer3.Core.Constants.ClaimTypes.Subject)?.Value);
            var userDb = userManager.FindById(id);
            if (!list.Any(c => c.Type == "forename") && userDb.ForeName != null)
                list.Add(new Claim("forename", userDb.ForeName));

            if (!list.Any(c => c.Type == "surname") && userDb.SurName != null)
                list.Add(new Claim("surname", userDb.SurName));

            if (!list.Any(c => c.Type == "country_code") && userDb.CountryCode != null)
                list.Add(new Claim("country_code", userDb.CountryCode));


            if (!list.Any(c => c.Type == "email") && userDb.Email != null)
                list.Add(new Claim("email", userDb.Email));


            if (!list.Any(c => c.Type == "phone_number") && userDb.PhoneNumber != null)
                list.Add(new Claim("phone_number", userDb.PhoneNumber));

            if (!list.Any(c => c.Type == "picture") && userDb.Picture != null)
                list.Add(new Claim("picture", PathUtils.GetNetPath(userDb.Picture)));


            if (!list.Any(c => c.Type == "address") && userDb.Address != null)
                list.Add(new Claim("address", userDb.Address));


            if (!list.Any(c => c.Type == "gender") && userDb.Gender != null)
                list.Add(new Claim("gender", userDb.Gender.ToString()));
        }
    }
}