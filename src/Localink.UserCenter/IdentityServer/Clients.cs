using IdentityServer3.Core.Models;
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
                        "https://localhost:44333/"
                    },
                    AllowedScopes = new List<string>
                    {
                        "openid",
                        "profile",
                        "roles",
                        "keyApi"
                    }
                }
            };
        }
    }
}