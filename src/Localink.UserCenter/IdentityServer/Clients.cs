using IdentityServer3.Core.Models;
using Localink.UserCenter.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Localink.UserCenter.IdentityServer
{
    public static class Clients
    {
        public static IEnumerable<Client> Get()
        {
            return new[] {
                new Client
                {
                    ClientName = "MVC Client",
                    ClientId="mvc",
                    Flow = Flows.Implicit,
                    //隐藏客户授权许可页面，默认授权
                    RequireConsent = false,
                    AllowAccessToAllScopes = true,
                    RedirectUris = new List<string> {
                        IdentityConfig.IdsrvRootAddress
                    },
                    PostLogoutRedirectUris = new List<string> {
                       IdentityConfig.IdsrvRootAddress
                    }
                },
                new Client
                {
                    ClientName = "Localink App Client",
                    ClientId="localink.ios.app",
                    ClientSecrets = new List<Secret> {
                        new Secret("55b7bab9-c741-47f7-9d50-f5a654386029".Sha256())
                    },
                    Flow = Flows.ResourceOwner,
                    //AllowedCorsOrigins = new List<string> {
                    //    "http://127.0.0.1:8020"
                    //},
                    
                    AllowedScopes = new List<string>
                    {
                        "openid",
                        "profile",
                        "roles",
                        "keyApi"
                    }
                },
            };
        }
    }
}