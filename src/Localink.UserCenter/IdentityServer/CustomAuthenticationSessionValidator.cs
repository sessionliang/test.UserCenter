using IdentityServer3.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Localink.UserCenter.IdentityServer
{
    public class CustomeAuthenticationSessionValidator : IAuthenticationSessionValidator
    {
        public bool Response { get; set; }

        public Task<bool> IsAuthenticationSessionValidAsync(ClaimsPrincipal subject)
        {
            return Task.FromResult(Response);
        }
    }
}